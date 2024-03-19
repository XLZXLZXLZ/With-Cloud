using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool //不继承类
{
    private static ObjectPool instance;
    //单例
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    //字典对应对象池(这里使用List也可以)
    private GameObject pool;
    //声明一个对象池游戏物体
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
                instance = new ObjectPool();
            return instance;
            //如果还没有设置单例,实例化一个,有的话直接返回
            //外界找Instance,Instance设置一个单例instance(即一个ObjectPool)给他,让外界用里边的函数
        }
    }
    public GameObject GetObj(GameObject prefab)
    {
        //从对象池中获取一个物体的方法,包含对初始化的一系列操作
        GameObject _object;
        //声明临时变量_object,用来处理对象池的一系列操作
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            //如果对象池不包含传入预制体名的key,或含该key但池中已经没有物体了,包含下列可能:
            //1.对象池游戏物体未创建  2.对象池已创建,缺少对应该物体的池子 3.该物体对象池物体不足

            _object = GameObject.Instantiate(prefab);
            //三种情况都必然要Instantiate物体

            PushObj(_object);
            //调用PushObj,压入对象池中

            if (pool == null)
                pool = new GameObject("ObjectPool");
            //如果没有对象池,创建一个

            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            //找对应该物体的子对象池

            if (childPool == null)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
                //没有找到,实例化一个,并将其设置未对象池的子物体
            }

            _object.transform.SetParent(childPool.transform);
            //经过以上步骤,场上必定已经有了对象池,附加于对象池的子对象池,这里再把声明的物体作为子物体附加到子对象池上
        }
        _object = objectPool[prefab.name].Dequeue();
        //经过以上步骤,场上必定有相应的对象池且对象池中含有至少一个相应的子物体,把其中一个赋值给_object
        if (_object.IsUnityNull())
            _object = (GetObj(prefab));
        //若该物体引用已被摧毁，递归调用直到可以

        _object.SetActive(true);
        return _object;
        //激活,然后返回
    }

    //有坐标和方向的重载
    public GameObject GetObj(GameObject prefab, Vector3 position,Quaternion rotation)
    {
        var _object = GetObj(prefab);
        _object.transform.position = position;
        _object.transform.rotation = rotation;
        return _object;
    }

    public void PushObj(GameObject prefab)
    {
        //向对象池压入物体的方法
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        //临时变量_name,获得prefab的name删去(Clone)

        if (!objectPool.ContainsKey(_name))
            objectPool.Add(_name, new Queue<GameObject>());
        //若字典不包含该_name对应的key,添加一个

        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
        //向对象池压入该物体,并取消激活
    }
}

