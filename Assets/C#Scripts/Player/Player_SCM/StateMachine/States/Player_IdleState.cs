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
        if (controller.IsMoving) //���ƶ����л�Ϊվ��
        {
            stateMachine.SwitchState(typeof(Player_RunState));
            return;
        }

        if (input.Jump) //������Ծ���л�Ϊ��Ծ
        {
            stateMachine.SwitchState(typeof(Player_JumpState));
            return;
        }

        if (!controller.IsGround) //�뿪���棬�л�Ϊ����״̬
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
