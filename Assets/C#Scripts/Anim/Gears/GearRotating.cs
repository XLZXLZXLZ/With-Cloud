using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotating : MonoBehaviour
{
    public bool isOn;
    private void Update()
    {
        if (isOn)
            transform.Rotate(0, 0, 360 * Time.deltaTime);
    }
}
