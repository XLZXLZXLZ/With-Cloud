using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 粒子效果处理器，这个脚本让它们结束时被压入对象池而非摧毁
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
