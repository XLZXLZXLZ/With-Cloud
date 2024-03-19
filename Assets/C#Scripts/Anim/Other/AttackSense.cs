using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 打击感脚本
/// 这个脚本负责处理顿帧，镜头摇晃等打击感效果
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
