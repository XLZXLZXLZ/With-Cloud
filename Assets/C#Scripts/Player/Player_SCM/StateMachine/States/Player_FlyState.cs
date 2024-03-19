using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fly", fileName = "Player_Fly")]
public class Player_FlyState : PlayerStates
{
    public float fallSpeed = 2f;
    public override void Enter()
    {
        base.Enter();
        anim.Play("Fly");
        //controller.SetVelocityY(fallSpeed);

        info.accelerateMult /= 1.5f;
        info.currentState = PlayerState.Fly;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!input.Jumping)
        {
            stateMachine.SwitchState(typeof(Player_FallState));
            return;
        }

        if (controller.IsGround)
        {
            stateMachine.SwitchState(typeof(Player_IdleState));
            return;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        controller.SetVelocityX(input.axes.x * info.speed);
        if(rb.velocity.y < -fallSpeed)
            controller.SetVelocityY(-fallSpeed);
    }

    public override void Exit()
    {
        base.Exit();

        info.accelerateMult *= 1.5f;
    }
}
