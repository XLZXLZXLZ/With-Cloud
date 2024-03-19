using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum PlayerState
{ 
    Idle,
    Move,
    Run,
    Jump,
    AirJump,
    Fall,
    Fly
}

public class PlayerInfo : Singleton<PlayerInfo>
{
    public float speed = 5;
    public float accelerate => speed * accelerateMult;
    public float accelerateMult = 3;
    public PlayerState currentState;
    public int airJumpChance = 1;

    public Vector2 baseSpeed; //�ƻ����˶��ٶȣ����������������ʱ���乲��

    [Header("����")]
    public bool flyAbility;

    public int holdingFire;
}
