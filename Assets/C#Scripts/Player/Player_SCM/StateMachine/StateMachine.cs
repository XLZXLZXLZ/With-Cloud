using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有状态机的基类
/// 请勿在子类中使用 Update() 和 FixedUpdate()
/// </summary>
public class StateMachine : MonoBehaviour
{
    //状态字典，获取状态用，它将由子类的数组赋值
    public Dictionary<System.Type, IState> stateMap;

    //当前状态
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
    /// 初始化时开启第一个状态,并调用其进入函数
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
    /// 状态之间进行切换时,调用当前状态的退出函数,切换状态,再调用新状态的进入函数
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
