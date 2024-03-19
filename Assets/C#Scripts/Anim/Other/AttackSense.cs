using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// ����нű�
/// ����ű��������֡����ͷҡ�εȴ����Ч��
/// </summary>
public class AttackSense : Singleton<AttackSense>
{
    public void CameraShake(float time, float intensity)
    {
        StartCoroutine(CameraShaking(time, intensity));
    }
    private IEnumerator CameraShaking(float time, float intensity)
    {
        float timer = 0;
        var camera = Camera.main.transform;
        if(camera == null)
            yield break;
        while(timer < time)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            camera.position += Random.insideUnitSphere * intensity;
        }
    }
}
