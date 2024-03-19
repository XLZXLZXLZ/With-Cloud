using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态类的基类,子类继承于ScriptableObject
/// </summary>
public interface IState
{
    /// <summary>
    /// 进入该状态执行的函数
    /// </summary>
    void Enter();

    /// <summary>
    /// 退出该状态执行的函数
    /// </summary>
    void Exit();

    /// <summary>
    /// 该状态下的逻辑更新
    /// </summary>
    void LogicUpdate();

    /// <summary>
    /// 该状态下的物理更新
    /// </summary>
    void PhysicsUpdate();
}
