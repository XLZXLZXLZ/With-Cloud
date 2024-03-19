using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool //���̳���
{
    private static ObjectPool instance;
    //����
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    //�ֵ��Ӧ�����(����ʹ��ListҲ����)
    private GameObject pool;
    //����һ���������Ϸ����
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
                instance = new ObjectPool();
            return instance;
            //�����û�����õ���,ʵ����һ��,�еĻ�ֱ�ӷ���
            //�����Instance,Instance����һ������instance(��һ��ObjectPool)����,���������ߵĺ���
        }
    }
    public GameObject GetObj(GameObject prefab)
    {
        //�Ӷ�����л�ȡһ������ķ���,�����Գ�ʼ����һϵ�в���
        GameObject _object;
        //������ʱ����_object,�����������ص�һϵ�в���
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            //�������ز���������Ԥ��������key,�򺬸�key�������Ѿ�û��������,�������п���:
            //1.�������Ϸ����δ����  2.������Ѵ���,ȱ�ٶ�Ӧ������ĳ��� 3.�������������岻��

            _object = GameObject.Instantiate(prefab);
            //�����������ȻҪInstantiate����

            PushObj(_object);
            //����PushObj,ѹ��������

            if (pool == null)
                pool = new GameObject("ObjectPool");
            //���û�ж����,����һ��

            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            //�Ҷ�Ӧ��������Ӷ����

            if (childPool == null)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
                //û���ҵ�,ʵ����һ��,����������δ����ص�������
            }

            _object.transform.SetParent(childPool.transform);
            //�������ϲ���,���ϱض��Ѿ����˶����,�����ڶ���ص��Ӷ����,�����ٰ�������������Ϊ�����帽�ӵ��Ӷ������
        }
        _object = objectPool[prefab.name].Dequeue();
        //�������ϲ���,���ϱض�����Ӧ�Ķ�����Ҷ�����к�������һ����Ӧ��������,������һ����ֵ��_object
        if (_object.IsUnityNull())
            _object = (GetObj(prefab));
        //�������������ѱ��ݻ٣��ݹ����ֱ������

        _object.SetActive(true);
        return _object;
        //����,Ȼ�󷵻�
    }

    //������ͷ��������
    public GameObject GetObj(GameObject prefab, Vector3 position,Quaternion rotation)
    {
        var _object = GetObj(prefab);
        _object.transform.position = position;
        _object.transform.rotation = rotation;
        return _object;
    }

    public void PushObj(GameObject prefab)
    {
        //������ѹ������ķ���
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        //��ʱ����_name,���prefab��nameɾȥ(Clone)

        if (!objectPool.ContainsKey(_name))
            objectPool.Add(_name, new Queue<GameObject>());
        //���ֵ䲻������_name��Ӧ��key,���һ��

        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
        //������ѹ�������,��ȡ������
    }
}

