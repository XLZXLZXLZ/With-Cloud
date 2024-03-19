using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 这是一个工具类
/// 会存储一些常用的功能，并以静态方法存储
/// </summary>
public class Tools : MonoBehaviour
{
    /// <summary>
    /// 获取鼠标位置的属性
    /// </summary>
    static public Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    /// <summary>
    /// 获取到玩家
    /// </summary>
    static public GameObject player => GameObject.FindGameObjectWithTag("Player");

    /// <summary>
    /// 制造一条三点的贝塞尔曲线
    /// </summary>
    /// <param name="origin">起点</param>
    /// <param name="destination">终点</param>
    /// <param name="adjustPoint">调整点</param>
    /// <param name="t">在该曲线上的百分比位置</param>
    /// <returns>该曲线上的点</returns>
    static public Vector2 BezierCurve(Vector2 origin, Vector2 destination, Vector2 adjustPoint, float t)
    {
        return Vector2.Lerp(Vector2.Lerp(origin, adjustPoint, t), Vector2.Lerp(adjustPoint, destination, t), t);
    }

    /// <summary>
    /// 获取该名字的层级
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    static public int GetLayer(string name)
    {
        return 1 << LayerMask.NameToLayer(name);
    }

    public static T[] GetAllComponentsInChildren<T>(Transform transform) where T : Component
    {
        T[] results = transform.GetComponentsInChildren<T>();
        for (int i = 0; i < transform.childCount;i++)
            results = results.Concat(GetAllComponentsInChildren<T>(transform.GetChild(i))).ToArray();
        return results;

        /*
        T component = transform.GetComponent<T>();

        // 递归获取所有子Transform上的指定组件
        T[] allComponentsInChildren = transform.GetComponentsInChildren<T>();
        if (allComponentsInChildren != null && allComponentsInChildren.Length > 0)
        {
            component = component == null ? allComponentsInChildren[0] : component;
            for (int i = 0; i < allComponentsInChildren.Length; i++)
            {
                if (allComponentsInChildren[i] != component)
                {
                    component = allComponentsInChildren[i];
                }
            }
        }

        // 返回获取到的所有组件
        return allComponentsInChildren;*/
    }
}

