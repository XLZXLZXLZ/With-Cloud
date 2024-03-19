using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnim : MonoBehaviour
{
    public float floatSpeed = 10f;
    public float rotateSpeed = 1f;
    private void Update()
    {
        transform.Rotate(1 * rotateSpeed * Time.deltaTime, 1.2F * rotateSpeed * Time.deltaTime, 1.3F * rotateSpeed * Time.deltaTime);
        transform.Translate(0, 0.001f * floatSpeed * Mathf.Sin(Time.time), 0);
    }
}
