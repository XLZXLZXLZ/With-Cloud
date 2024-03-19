using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEyeAnim : MonoBehaviour
{
    public float rotateSpeed = 3.0f;

    private PlayerInfo info;
    private float angle = 0;

    private void Update()
    {
        //��������������˿���ķ���ǿ����÷����޸�
        if (Input.GetKey(KeyCode.UpArrow))
            angle = 8;
        else if (Input.GetKey(KeyCode.DownArrow))
            angle = -8;
        else
            angle = 0;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);
    }
}
