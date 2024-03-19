using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ����һ��������
/// ��洢һЩ���õĹ��ܣ����Ծ�̬�����洢
/// </summary>
public class Tools : MonoBehaviour
{
    /// <summary>
    /// ��ȡ���λ�õ�����
    /// </summary>
    static public Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    /// <summary>
    /// ��ȡ�����
    /// </summary>
    static public GameObject player => GameObject.FindGameObjectWithTag("Player");

    /// <summary>
    /// ����һ������ı���������
    /// </summary>
    /// <param name="origin">���</param>
    /// <param name="destination">�յ�</param>
    /// <param name="adjustPoint">������</param>
    /// <param name="t">�ڸ������ϵİٷֱ�λ��</param>
    /// <returns>�������ϵĵ�</returns>
    static public Vector2 BezierCurve(Vector2 origin, Vector2 destination, Vector2 adjustPoint, float t)
    {
        return Vector2.Lerp(Vector2.Lerp(origin, adjustPoint, t), Vector2.Lerp(adjustPoint, destination, t), t);
    }

    /// <summary>
    /// ��ȡ�����ֵĲ㼶
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

        // �ݹ��ȡ������Transform�ϵ�ָ�����
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

        // ���ػ�ȡ�����������
        return allComponentsInChildren;*/
    }
}

