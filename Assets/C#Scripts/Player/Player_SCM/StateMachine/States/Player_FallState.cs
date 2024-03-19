using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "Player_Fall")]
public class Player_FallState : PlayerStates
{
    public override void Enter()
    {
        base.Enter();
        
        info.currentState = PlayerState.Fall;
        rb.gravityScale *= 1.5f;
        anim.Play("Idle");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (controller.IsWall && input.Jump)
        {
            controller.SetVelocityX(-player.transform.localScale.x * info.speed);
            stateMachine.SwitchState(typeof(Player_JumpState));
            return;
        }

        if (input.Jump)
        {
            //�����������ã����ж�������������ԾԤ����
            if (controller.airJump)
            {
                stateMachine.SwitchState(typeof(Player_AirJumpState));
                controller.airJumpChance-- ;
                return;
            }
            else if (info.flyAbility)
            {
                stateMachine.SwitchState(typeof(Player_FlyState));
                return;
            }
            else
                input.JumpBufferInput();   //��ԾԤ���룬�ڽӽ����水����ԾʱҲ������ҽ�����һ����Ծ����ֹ�̼�
        }

        if (controller.IsGround)
        {
            if (input.Jump || input.JumpBuffer) //��ص�֡������Ծ�������ԾԤ���룬�ٴ���Ծ
            {
                stateMachine.SwitchState(typeof(Player_JumpState));
                controller.airJumpChance = 1;
                return;
            }
            else if (controller.IsMoving)  //���֡δ��Ծ���ƶ�����Ϊ�ƶ�״̬
                stateMachine.SwitchState(typeof(Player_RunState));
            else if (!controller.IsMoving) //���֡δ��Ծ�ƶ�����Ϊվ��״̬
                stateMachine.SwitchState(typeof(Player_IdleState));
        }

    }
    public override void Exit()
    {
        base.Exit();
        rb.gravityScale /= 1.5f;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(!(controller.IsWall && input.axes.x * player.transform.localScale.x > 0) && StateDuration >= 0.05f)
            controller.SetVelocityX(input.axes.x * info.speed);
    }

}
