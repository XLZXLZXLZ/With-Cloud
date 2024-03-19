using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FireSwitch : Switch
{
    ParticleSystem fire;
    private float rate;
    private void Start()
    {
        fire = GetComponentInChildren<ParticleSystem>();
        EmissionModule emission = fire.emission;
        rate = emission.rateOverTime.constantMax;
        emission.rateOverTime = new MinMaxCurve(0f);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.TryGetComponent<FireBall>(out var fireBall))
        {
            EmissionModule emission = fire.emission;
            emission.rateOverTime = new MinMaxCurve(rate);
            switchOn();
            isOn = true;
        }
    }
}
