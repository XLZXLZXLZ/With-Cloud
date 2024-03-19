using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBoard : Board
{
    public Vector3 rotationAxis;
    public float rotationAngle;
    public float rotationSpeed;

    public bool alwaysWorking;

    private float currentRotation;
    private bool isRotating;

    private void Start()
    {
        if(alwaysWorking)
        {
            isRotating= true;
            gear.isOn = true;
            currentRotation=0;
            rotationAngle = 999999999;
        }
    }
    private void FixedUpdate()
    {
        if (isRotating)
        {
            float angleToRotate = rotationSpeed * Time.fixedDeltaTime;
            if (Mathf.Abs(currentRotation + angleToRotate) >= Mathf.Abs(rotationAngle))
            {
                angleToRotate = rotationAngle - currentRotation;
                isRotating = false;
                gear.isOn = false;
            }
            transform.RotateAround(transform.TransformPoint(rotationAxis), transform.forward, angleToRotate);
            currentRotation += angleToRotate;
        }
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();

        currentRotation = 0f;
        isRotating = true;
        gear.isOn = true;
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();

        currentRotation = 0f;
        isRotating = false;
        gear.isOn = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(rotationAxis));

        float radius = 0.5f;
        float angle = currentRotation / rotationAngle * 360f;
        Quaternion rotation = Quaternion.AngleAxis(angle, transform.up);
        Vector3 startPoint = transform.TransformPoint(rotationAxis) + rotation * Vector3.forward * radius;
        Vector3 endPoint = transform.TransformPoint(rotationAxis) + rotation * Vector3.back * radius;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPoint, endPoint);
        Gizmos.DrawWireSphere(startPoint, radius);
        Gizmos.DrawWireSphere(endPoint, radius);

        Gizmos.color = Color.red;
        if (targetSwitch != null)
            Gizmos.DrawLine(transform.position, targetSwitch.transform.position);
    }
}
