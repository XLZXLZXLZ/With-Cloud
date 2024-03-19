using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run",fileName = "Player_Run")]
public class Player_RunState : PlayerStates
{
    ParticleSystem particle;
    public override void Enter()
    {
        base.Enter();
        particle = player.transform.Find("MoveParticle").gameObject.GetComponent<ParticleSystem>();
        ParticleSystem ps = particle;
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(8f);
        info.currentState = PlayerState.Run;

        controller.airJumpChance = info.airJumpChance;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!controller.IsMoving) //不移动，切换为站立
        {
            stateMachine.SwitchState(typeof(Player_IdleState));
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
        if(!(controller.IsWall && input.axes.x * player.transform.localScale.x > 0))
            controller.SetVelocityX(input.axes.x * info.speed);
    }

    public override void Exit()
    {
        base.Exit();
        ParticleSystem ps = particle;
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(0);
    }

}
