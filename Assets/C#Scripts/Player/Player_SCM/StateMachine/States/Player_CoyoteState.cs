using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 土狼时间，玩家越过平台后的短暂滞空状态,允许玩家在这段滞空时间内跳跃
/// </summary>
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Coyote", fileName = "Player_Coyote")]
public class Player_CoyoteState : PlayerStates
{
    public float coyoteTimeLength = 0.1f; //土狼时间长度
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (input.Jump) //按下跳跃，允许在土狼时间跳跃
            stateMachine.SwitchState(typeof(Player_JumpState));
        if (!controller.IsGround && StateDuration>=coyoteTimeLength) //超时且不触地，切换为掉落状态
            stateMachine.SwitchState(typeof(Player_FallState));
        if (controller.IsGround && StateDuration >= coyoteTimeLength)
            stateMachine.SwitchState(typeof(Player_IdleState));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        controller.SetVelocityX(input.axes.x * 8);
    }
}
