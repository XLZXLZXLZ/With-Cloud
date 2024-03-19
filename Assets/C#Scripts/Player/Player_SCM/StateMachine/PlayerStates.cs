using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态基类,所有玩家状态继承于该基类
/// </summary>
public class PlayerStates : ScriptableObject, IState
{
    //玩家状态机
    public PlayerStateMachine stateMachine;

    //玩家动画器组件
    public Animator anim;

    //玩家刚体组件
    public Rigidbody2D rb;

    //玩家刚体组件
    public PlayerControl controller;

    //玩家操作表组件
    public PlayerInput input;

    //玩家游戏物体
    public GameObject player;

    //玩家信息组件
    public PlayerInfo info;

    //状态持续时间
    public float StateDuration => Time.time - stateStartTime;
    private float stateStartTime;


    /*
     * 因为ScriptableObject不挂载在玩家身上，所以需要将玩家身上组件以引用类型赋值给状态
     * 以上为初始化时准备好的引用类型，负责不同状态对玩家的操控
     * 第一个是状态机，因为状态切换需要调用玩家身上的状态机
     * 第二，三，四个为玩家身上的常用组件，由于常用，在此也赋值好
     * 第五个为玩家本private 身的游戏物体，在不得已的情况下可以直接用player.GetComponent<T>来找到相应组件
     */

    /// <suublic
    /// 初始化函数，状态机启用时，对所有状态进行初始化，将玩家身上的组件赋值进去
    /// </summary>
    public void Initialize(PlayerStateMachine stateMachine, Animator animator, PlayerControl controller, PlayerInput input,Rigidbody2D rb,GameObject player)
    {
        this.stateMachine = stateMachine;
        this.anim = animator;
        this.controller = controller;
        this.input = input;
        this.rb = rb;
        this.player = player;
        this.info = PlayerInfo.Instance;
    }

    public virtual void Enter()
    {
        stateStartTime = Time.time;
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() 
    {

    }

    public virtual void PhysicsUpdate() { }

}
