using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class GreenLazer : MonoBehaviour
{
    LineRenderer line;

    public float maxDistance;
    public GameObject targetSwitch;

    private Switch target;
    private static bool isCalm;
    private Transform destination;
    private bool isOn = true;

    private float lineWidth;
    private int width = 1;
    private float lastLength;

    private void Awake()
    {
        line = GetComponentInChildren<LineRenderer>();
        lineWidth = line.startWidth;
        destination = transform.Find("Destination");

        if (targetSwitch == null)
            return;
        target = targetSwitch.GetComponent<Switch>();
        target.switchOn += SwitchOn;
        target.switchOff += SwitchOff;
    }
    private void Start()
    {
        RaycastHit2D target;
        target = Physics2D.Raycast(transform.position, transform.up, maxDistance, Tools.GetLayer("Ground"));
        Vector2 des;
        if (!target)
            des = transform.position + transform.up * maxDistance;
        else
            des = target.point;

        destination.position = des;
    }
    private void Update()
    {
        line.startWidth = Mathf.MoveTowards(line.startWidth,lineWidth * width,Time.deltaTime * 2);

        if (!isOn)
            return;
        RaycastHit2D target;
        if (CloudInfo.Instance.currentState == CloudState.Green)
            target = Physics2D.Raycast(transform.position, transform.up, maxDistance, Tools.GetLayer("Ground") | Tools.GetLayer("Cloud"));
        else
            target = Physics2D.Raycast(transform.position, transform.up, maxDistance, Tools.GetLayer("Ground"));

        Vector2 des;
        if (!target)
        {
            des = transform.position + transform.up * maxDistance;
            des = Vector3.Lerp(destination.transform.position, des, 30 * Time.deltaTime);
        }
        else if (target)
        {
            float length = ((Vector3)target.point - transform.position).magnitude;
            if (length < lastLength)
                des = target.point;
            else
                des = Vector3.Lerp(destination.transform.position, target.point, 30 * Time.deltaTime);
        }
        else
            des = Vector2.zero;

        //Vector2 des = target ? Vector3.MoveTowards(destination.transform.position,target.point,50*Time.deltaTime) : transform.position + transform.up * maxDistance;


        line.SetPosition(0, transform.position);
        line.SetPosition(1, des);
        destination.position = des;
        lastLength = (destination.transform.position - transform.position).magnitude;

        RaycastHit2D player = Physics2D.Linecast(transform.position, des, Tools.GetLayer("Player"));
        if (player)
            player.collider.GetComponent<PlayerDeath>().Death();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * maxDistance);

        if (targetSwitch != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetSwitch.transform.position);
        }
    }

    private void SwitchOn()
    {
        destination.gameObject.SetActive(false);
        width = 0;
        isOn = false;
    }

    private void SwitchOff()
    {
        destination.gameObject.SetActive(true);
        width = 1;
        isOn = true;
    }
}
