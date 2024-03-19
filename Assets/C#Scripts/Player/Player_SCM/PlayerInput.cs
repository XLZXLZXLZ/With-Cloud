using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput: MonoBehaviour
{
    //玩家输入
    PlayerInputActions action;

    //WSAD操作轴
    public Vector2 axes => action.GamePlay.Move.ReadValue<Vector2>();
    public Vector2 cloudAxes => action.GamePlay.Cloud.ReadValue<Vector2>();
    //跳跃
    public bool Jump => action.GamePlay.Jump.WasPressedThisFrame();

    //按住跳跃键
    public bool Jumping => action.GamePlay.Jump.IsPressed();

    //停止跳跃
    public bool StopJump => action.GamePlay.Jump.WasReleasedThisFrame();

    //跳跃预输入
    public bool JumpBuffer = false;

    //技能输入
    public bool CloudSkill => action.GamePlay.CloudSkill.WasPressedThisFrame();


    //玩家输入动作表实例化
    private void Awake()
    {
        action = new PlayerInputActions();
    }

    //是否开启玩家动作表
    public void InputEnable(bool isOn)
    {
        if (isOn)
            action.GamePlay.Enable();
        else
            action.GamePlay.Disable();
    }

    //玩家跳跃预输入及相应协程
    public void JumpBufferInput()
    {
        StopCoroutine(nameof(JumpBufferHold));
        StartCoroutine(nameof(JumpBufferHold));
    }

    private IEnumerator JumpBufferHold()
    {
        JumpBuffer = true;
        yield return new WaitForSecondsRealtime(0.25f);
        JumpBuffer = false;
    }
}
