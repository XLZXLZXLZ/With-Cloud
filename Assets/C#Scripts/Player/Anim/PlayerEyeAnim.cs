using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEyeAnim : MonoBehaviour
{
    public float rotateSpeed = 3.0f;

    private PlayerInfo info;
    private float angle = 0;

    private void Awake()
    {
        info = GetComponentInParent<PlayerInfo>();
    }

    private void Update()
    {
        //Ĭ�ϸ��ݵ�ǰ״̬�޸ĽǶ�
        switch (info.currentState)
        {
            case PlayerState.AirJump:
            case PlayerState.Jump: angle = 15; break;
            case PlayerState.Fall: angle = -30; break;
            case PlayerState.Fly: angle = -20; break;
            case PlayerState.Idle:
            case PlayerState.Run: angle = 0; break;
        }

        //��������������˿���ķ���ǿ����÷����޸�
        if (Input.GetKey(KeyCode.W))
            angle = 20;
        else if (Input.GetKey(KeyCode.S))
            angle = -20;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);
    }
}
