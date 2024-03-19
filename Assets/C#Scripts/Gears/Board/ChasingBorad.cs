using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBorad : Board
{
    public Vector3 destination;
    public Vector3 origin;
    public bool isLerp = false;
    public float maxSpeed = 8;
    public float minSpeed = 2;

    private Vector3 currentTarget;
    private bool isMoving;
    private Transform player;

    public float speed;
    public bool alwaysWorking;

    private void Start()
    {
        origin = transform.position;
        destination = transform.position + destination;
        player = Tools.player.transform;
        if (alwaysWorking)
            SwitchOn();
        if (BGMManager.Instance.currentBGM != "Maze")
            BGMManager.Instance.StopBGM(1);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            float distance = player.transform.position.x - transform.position.x;
            speed = Mathf.Clamp(minSpeed + (distance - 18) * 0.25f, minSpeed, maxSpeed);

            transform.position = isLerp ? Vector3.Lerp(transform.position, currentTarget, speed * Time.fixedDeltaTime) : Vector3.MoveTowards(transform.position, currentTarget, speed * Time.fixedDeltaTime);
            if ((transform.position - currentTarget).sqrMagnitude <= 0.01f)
            {
                isMoving = false;
                gear.isOn = false;
            }
        }
    }

    //在检查点生成时调用这个(狠狠滴堆屎山)
    public void CheckOn()
    {
        SwitchOn();
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();

        AttackSense.Instance.CameraShake(3, 0.3f);
        AttackSense.Instance.CameraShake(500, 0.12f);
        currentTarget = destination;
        gear.isOn = true;
        isMoving = true;
        StartCoroutine(BGMPlaying());
    }

    private IEnumerator BGMPlaying()
    {
        yield return new WaitForSeconds(1);
        BGMManager.Instance.PlayBGM("Maze", 1, 0.8f);
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
