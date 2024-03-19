using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/AirJump", fileName = "Player_AirJump")]
public class Player_AirJumpState : PlayerStates
{
    public GameObject jumpParticle;
    public float jumpVelocity = 18f;
    public override void Enter()
    {
        base.Enter();
        input.JumpBuffer = false;
        ObjectPool.Instance.GetObj(jumpParticle, player.transform.position - new Vector3(0, 1f, 0), Quaternion.identity);
        anim.Play("AirJump");

        info.currentState = PlayerState.AirJump;

        controller.SetVelocityY(jumpVelocity);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (controller.IsWall && input.Jump)
        {
            controller.SetVelocityX(player.transform.localScale.x * info.speed);
            stateMachine.SwitchState(typeof(Player_JumpState));
            return;
        }

        if (input.Jump)
        {
            //�����������ã����ж�������������ԾԤ����
            if (controller.airJump && StateDuration >= 0.1f)
            {
                stateMachine.SwitchState(typeof(Player_AirJumpState));
                controller.airJumpChance--;
                return;
            }
            else if (info.flyAbility && StateDuration >= 0.1f)
            {
                stateMachine.SwitchState(typeof(Player_FlyState));
                return;
            }
            else
                input.JumpBufferInput();    //��ԾԤ���룬�ڽӽ����水����ԾʱҲ������ҽ�����һ����Ծ����ֹ�̼�
        }

        //�����ٶȹ���/��ײ�컨��/���ֹͣ���ո�ʱ,����׹��״̬
        if (/*(!input.Jumping && StateDuration>=0.2f) || */controller.IsFalling)
            stateMachine.SwitchState(typeof(Player_FallState));
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(!(controller.IsWall && input.axes.x * player.transform.localScale.x > 0))
            controller.SetVelocityX(input.axes.x * info.speed);
    }
}
