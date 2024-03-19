using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloatAnim : MonoBehaviour
{
    private void Update()
    {
        transform.localPosition = new Vector3(0, 0.1f + 0.1f * Mathf.Sin(Time.time*1.25f), 0);
    }
}
