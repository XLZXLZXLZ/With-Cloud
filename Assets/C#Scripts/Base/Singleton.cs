using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��������,ֻ��̳иû��༴�ɱ�Ϊ����,�÷�ͬ��������
/// </summary>
/// <typeparam name="T">��������</typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance //����
    {
        get
        {
            if (instance == null || instance.IsUnityNull())
            {
                instance = FindObjectOfType<T>();
                if (instance == null || instance.IsUnityNull())
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }
}