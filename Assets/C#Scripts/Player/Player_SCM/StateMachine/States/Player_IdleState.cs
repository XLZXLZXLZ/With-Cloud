using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "Player_Idle")]
public class Player_IdleState : PlayerStates
{
    public override void Enter()
    {
        base.Enter();
        anim.Play("Idle");
        controller.SetVelocityX(0);

        controller.airJumpChance = info.airJumpChance;

        info.currentState = PlayerState.Idle;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (controller.IsMoving) //不移动，切换为站立
        {
            stateMachine.SwitchState(typeof(Player_RunState));
            return;
        }

        if (input.Jump) //按下跳跃，切换为跳跃
        {
            stateMachine.SwitchState(typeof(Player_JumpState));
            return;
        }

        if (!controller.IsGround) //离开地面，切换为土狼状态
        {
            stateMachine.SwitchState(typeof(Player_CoyoteState));
            return;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        controller.SetVelocityX(0);
    }
}
