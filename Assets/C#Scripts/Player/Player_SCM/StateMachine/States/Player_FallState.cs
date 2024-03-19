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
            //若二段跳可用，进行二段跳，否则跳跃预输入
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
                input.JumpBufferInput();   //跳跃预输入，在接近地面按下跳跃时也允许玩家进行下一次跳跃，防止吞键
        }

        if (controller.IsGround)
        {
            if (input.Jump || input.JumpBuffer) //落地当帧按下跳跃或存在跳跃预输入，再次跳跃
            {
                stateMachine.SwitchState(typeof(Player_JumpState));
                controller.airJumpChance = 1;
                return;
            }
            else if (controller.IsMoving)  //落地帧未跳跃但移动，变为移动状态
                stateMachine.SwitchState(typeof(Player_RunState));
            else if (!controller.IsMoving) //落地帧未跳跃移动，变为站立状态
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
