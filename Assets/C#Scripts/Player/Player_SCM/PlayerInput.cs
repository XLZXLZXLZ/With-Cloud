using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput: MonoBehaviour
{
    //�������
    PlayerInputActions action;

    //WSAD������
    public Vector2 axes => action.GamePlay.Move.ReadValue<Vector2>();
    public Vector2 cloudAxes => action.GamePlay.Cloud.ReadValue<Vector2>();
    //��Ծ
    public bool Jump => action.GamePlay.Jump.WasPressedThisFrame();

    //��ס��Ծ��
    public bool Jumping => action.GamePlay.Jump.IsPressed();

    //ֹͣ��Ծ
    public bool StopJump => action.GamePlay.Jump.WasReleasedThisFrame();

    //��ԾԤ����
    public bool JumpBuffer = false;

    //��������
    public bool CloudSkill => action.GamePlay.CloudSkill.WasPressedThisFrame();


    //������붯����ʵ����
    private void Awake()
    {
        action = new PlayerInputActions();
    }

    //�Ƿ�����Ҷ�����
    public void InputEnable(bool isOn)
    {
        if (isOn)
            action.GamePlay.Enable();
        else
            action.GamePlay.Disable();
    }

    //�����ԾԤ���뼰��ӦЭ��
    public void JumpBufferInput()
    {
        StopCoroutine(nameof(JumpBufferHold));
        StartCoroutine(nameof(JumpBufferHold));
    }

    private IEnumerator JumpBufferHold()
    {
        JumpBuffer = true;
        yield return new WaitForSecondsRealtime(0.25f);
        JumpBuffer = false;
    }
}
