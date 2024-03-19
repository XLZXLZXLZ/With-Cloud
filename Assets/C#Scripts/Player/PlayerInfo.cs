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

    public Vector2 baseSpeed; //云基座运动速度，即当玩家在云身上时与其共速

    [Header("能力")]
    public bool flyAbility;

    public int holdingFire;
}
