using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject targetSwitch;

    private Switch target;

    protected GearRotating gear;
    private void Awake()
    {
        gear = GetComponentInChildren<GearRotating>();
        if (targetSwitch == null)
            return;
        targetSwitch.TryGetComponent(out target);
        target.switchOn += SwitchOn;
        target.switchOff += SwitchOff;
    }

    protected virtual void SwitchOn()
    {

    }

    protected virtual void SwitchOff()
    {

    }
}
