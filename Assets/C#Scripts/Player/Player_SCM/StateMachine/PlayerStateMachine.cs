using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// ���״̬��,�̳���״̬������,���ڶ���ҵ�����״̬�����ܵ���
/// </summary>
public class PlayerStateMachine : StateMachine
{

    //״̬���飬�洢����״̬
    public PlayerStates[] states;

    //��ʼ��״̬�ֵ�(��״̬�����ֵ�����״̬�� <���� ��> ����ʽ��ֵ��ȥ)
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
