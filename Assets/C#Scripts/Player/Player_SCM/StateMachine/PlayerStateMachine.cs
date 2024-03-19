using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// 玩家状态机,继承于状态机基类,用于对玩家的所有状态进行总调度
/// </summary>
public class PlayerStateMachine : StateMachine
{

    //状态数组，存储所有状态
    public PlayerStates[] states;

    //初始化状态字典(将状态数组种的所有状态以 <类名 类> 的形式赋值进去)
    private void Start()
    {
        var anim = GetComponent<Animator>();
        var controller = GetComponent<PlayerControl>();
        var input = GetComponent<PlayerInput>();
        var rb = GetComponent<Rigidbody2D>();
        var player = gameObject;

        stateMap = new Dictionary<System.Type, IState>();
        foreach (var state in states)
        {
            var s = Instantiate(state);
            stateMap.Add(state.GetType(), s);
            s.Initialize(this, anim, controller, input, rb ,player);
        }

        Begin(stateMap[typeof(Player_IdleState)]);
    }

    public void AddState(PlayerStates newState)
    {
        var anim = GetComponent<Animator>();
        var controller = GetComponent<PlayerControl>();
        var input = GetComponent<PlayerInput>();
        var rb = GetComponent<Rigidbody2D>();
        var player = gameObject;

        var s = Instantiate(newState);
        stateMap.Add(newState.GetType(), newState);
        s.Initialize(this, anim, controller, input, rb, player);
    }
}
