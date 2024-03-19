using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ״̬��Ļ���,����̳���ScriptableObject
/// </summary>
public interface IState
{
    /// <summary>
    /// �����״ִ̬�еĺ���
    /// </summary>
    void Enter();

    /// <summary>
    /// �˳���״ִ̬�еĺ���
    /// </summary>
    void Exit();

    /// <summary>
    /// ��״̬�µ��߼�����
    /// </summary>
    void LogicUpdate();

    /// <summary>
    /// ��״̬�µ��������
    /// </summary>
    void PhysicsUpdate();
}
