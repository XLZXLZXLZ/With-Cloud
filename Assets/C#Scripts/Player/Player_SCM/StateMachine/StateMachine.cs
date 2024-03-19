using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬���Ļ���
/// ������������ʹ�� Update() �� FixedUpdate()
/// </summary>
public class StateMachine : MonoBehaviour
{
    //״̬�ֵ䣬��ȡ״̬�ã���������������鸳ֵ
    public Dictionary<System.Type, IState> stateMap;

    //��ǰ״̬
    public IState currentState;

    private void Update()
    {
        currentState.LogicUpdate();
        //print(currentState);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    /// <summary>
    /// ��ʼ��ʱ������һ��״̬,����������뺯��
    /// </summary>
    public virtual void Begin(IState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public virtual void Begin(System.Type stateType)
    {
        Begin(stateMap[stateType]);
    }
    /// <summary>
    /// ״̬֮������л�ʱ,���õ�ǰ״̬���˳�����,�л�״̬,�ٵ�����״̬�Ľ��뺯��
    /// </summary>
    public virtual void SwitchState(IState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public virtual void SwitchState(System.Type stateType)
    {
        if(stateMap.ContainsKey(stateType))
            SwitchState(stateMap[stateType]);
    }
}
