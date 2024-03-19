using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "Player_Jump")]
public class Player_JumpState : PlayerStates
{
    public GameObject jumpParticle;
    public float jumpVelocity = 12f;
    public override void Enter()
    {
        base.Enter();
        input.JumpBuffer = false;
        ObjectPool.Instance.GetObj(jumpParticle, player.transform.position - new Vector3(0, 1f, 0),Quaternion.identity);
        anim.Play("Jump");

        info.currentState = PlayerState.Jump;
        
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
            if (controller.airJump && StateDuration >= 0.3f)
            {
                stateMachine.SwitchState(typeof(Player_AirJumpState));
                controller.airJumpChance--;
                return;
            }
            else if(info.flyAbility && StateDuration >= 0.3f)
            {
                stateMachine.SwitchState(typeof(Player_FlyState));
                return;
            }
            else
                input.JumpBufferInput();    //��ԾԤ���룬�ڽӽ����水����ԾʱҲ������ҽ�����һ����Ծ����ֹ�̼�
        }
            
        //�����ٶȹ���ʱ,����׹��״̬
        if (controller.IsFalling)
            stateMachine.SwitchState(typeof(Player_FallState));

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(StateDuration>=0.2f && !(controller.IsWall && input.axes.x * player.transform.localScale.x > 0))
            controller.SetVelocityX(input.axes.x * info.speed);
    }
}
