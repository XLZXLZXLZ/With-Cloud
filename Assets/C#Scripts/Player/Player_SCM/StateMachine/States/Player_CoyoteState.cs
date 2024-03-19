using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʱ�䣬���Խ��ƽ̨��Ķ����Ϳ�״̬,�������������Ϳ�ʱ������Ծ
/// </summary>
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Coyote", fileName = "Player_Coyote")]
public class Player_CoyoteState : PlayerStates
{
    public float coyoteTimeLength = 0.1f; //����ʱ�䳤��
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (input.Jump) //������Ծ������������ʱ����Ծ
            stateMachine.SwitchState(typeof(Player_JumpState));
        if (!controller.IsGround && StateDuration>=coyoteTimeLength) //��ʱ�Ҳ����أ��л�Ϊ����״̬
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
