using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float maxTime;

    private float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > maxTime)
            Destroy(gameObject);
    }
}
