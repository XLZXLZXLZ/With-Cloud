using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoard : Board
{
    public Vector3 destination;
    public Vector3 origin;
    public bool isLerp = false;
    public float distance;
    
    private Vector3 currentTarget;
    private bool isMoving;

    public float speed;
    public bool alwaysWorking;

    private void Start()
    {
        origin = transform.position;
        destination = transform.position + destination;
        if (alwaysWorking)
            SwitchOn();
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            transform.position = isLerp? Vector3.Lerp(transform.position, currentTarget, speed * Time.fixedDeltaTime) : Vector3.MoveTowards(transform.position, currentTarget, speed * Time.fixedDeltaTime);
            if ((transform.position - currentTarget).sqrMagnitude <= 0.01f)
            {
                isMoving = false;
                gear.isOn = false;
            }
        }
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();

        currentTarget = destination;
        gear.isOn = true;
        isMoving = true;
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();

        currentTarget = origin;
        gear.isOn = true;
        isMoving = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(destination, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + destination);

        if (targetSwitch != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetSwitch.transform.position, 0.5f);
            Gizmos.DrawLine(transform.position, targetSwitch.transform.position);
        }
    }
}
