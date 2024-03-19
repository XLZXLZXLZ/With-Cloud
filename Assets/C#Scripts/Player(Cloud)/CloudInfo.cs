using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum CloudState
{
    Red,
    Blue,
    Green,
    Default
}

public class CloudInfo : Singleton<CloudInfo>
{
    public GameObject[] stateParticles;
    public Color[] stateColor;
    
    public float velocity = 5;
    public float accelerate => velocity * accelerateMult;
    public float accelerateMult = 4;

    public CloudState currentState = CloudState.Default;


    private Color currentColor = Color.white;

    private SpriteRenderer sr;
    private ParticleSystem ps;

    private void Awake()
    {
        sr = transform.Find("Body").GetComponent<SpriteRenderer>();
        ps = transform.Find("MoveParticle").GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        Color c = Color.Lerp(sr.color, currentColor, Time.deltaTime * 3);
        sr.color = c;
        ParticleSystem.MainModule main = ps.main;
        main.startColor = c;
    }
    public void ChangeState(CloudState newState)
    {
        currentState = newState;
        ObjectPool.Instance.GetObj(stateParticles[(int)newState],transform.position,Quaternion.identity);
        currentColor = stateColor[(int)newState];
    }
}
