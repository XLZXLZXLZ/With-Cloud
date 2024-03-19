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
        //默认根据当前状态修改角度
        switch (info.currentState)
        {
            case PlayerState.AirJump:
            case PlayerState.Jump: angle = 15; break;
            case PlayerState.Fall: angle = -30; break;
            case PlayerState.Fly: angle = -20; break;
            case PlayerState.Idle:
            case PlayerState.Run: angle = 0; break;
        }

        //若玩家自行输入了看向的方向，强制向该方向修改
        if (Input.GetKey(KeyCode.W))
            angle = 20;
        else if (Input.GetKey(KeyCode.S))
            angle = -20;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);
    }
}
