using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ч��������������ű������ǽ���ʱ��ѹ�����ض��Ǵݻ�
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class ParticleActions : MonoBehaviour
{
    ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        particle.Play();
    }
    private void Update()
    {
        if (!particle.isPlaying)
            ObjectPool.Instance.PushObj(gameObject);
    }
}
