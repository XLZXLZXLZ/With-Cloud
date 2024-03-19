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

        if (!controller.IsMoving) //���ƶ����л�Ϊվ��
        {
            stateMachine.SwitchState(typeof(Player_IdleState));
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
