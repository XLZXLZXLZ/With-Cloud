using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowActions : MonoBehaviour
{
    public float[] angles;
    public int index;


    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(transform.eulerAngles.z, angles[index],Time.deltaTime * 5f));
    }
}
