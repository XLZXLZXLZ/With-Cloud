using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.ParticleSystem;

//传送技能效果脚本
public class Transport : MonoBehaviour
{
    public TransportBase last;

    public void OnPointChanged(TransportBase newBase)
    {
        if (last == newBase)
            return;
        if(last != null)
            last.OnCancelled();
        last= newBase;
        last.OnActived();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StopCoroutine(nameof(Transporting));
            StartCoroutine(nameof(Transporting));
        }
    }

    private IEnumerator Transporting()
    {
        float timer = 0;
        while(timer<1)
        {
            if (Input.GetKeyUp(KeyCode.R))
                yield break;
            timer += Time.deltaTime;
            yield return null;
        }
        ObjectPool.Instance.GetObj(last.particle, transform.position, Quaternion.identity);
        transform.position = last.transform.position;
        ObjectPool.Instance.GetObj(last.particle, transform.position, Quaternion.identity);
    }
}
