using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFloatAnim : MonoBehaviour
{
    void Update()
    {
        transform.localPosition += new Vector3(0, 0.005f * Mathf.Sin(Time.time * 1.5f), 0);
    }
}
