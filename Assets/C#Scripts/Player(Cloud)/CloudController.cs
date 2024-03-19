using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloudController : MonoBehaviour
{
    CloudInfo info;
    PlayerInput input;
    Rigidbody2D rb;
    Animation anim;

    public bool isTouching = false;

    private PlayerInfo p_info;
    private CloudSkill skill;
    private float leftEdge,rightEdge,topEdge,bottomEdge;

    private void Awake()
    {
        info = GetComponent<CloudInfo>();
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        skill = GetComponent<CloudSkill>();
        anim = GetComponent<Animation>();
        p_info = PlayerInfo.Instance;
    }

    private void Start()
    {
        input.InputEnable(true);
        leftEdge = CameraActions.Instance.leftEdge;
        rightEdge= CameraActions.Instance.rightEdge;
        topEdge = CameraActions.Instance.topEdge;
        bottomEdge = CameraActions.Instance.bottomEdge;
    }

    //调整速度与调整朝向
    private void SetVelocity(Vector2 velocity)
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, velocity, Time.deltaTime * info.accelerate);
        FaceAdjust(velocity.x);
    }
    private void FaceAdjust(float value)
    {
        var x = Mathf.Abs(transform.localScale.x);
        var y = Mathf.Abs(transform.localScale.y);
        var z = Mathf.Abs(transform.localScale.z);
        if (value > 0.1f)
            transform.localScale = new Vector3(x, y, z);
        else if (value < -0.1f)
            transform.localScale = new Vector3(-x, y, z);
    }

    //逻辑更新
    private void Update()
    {
        var axes = input.cloudAxes;
        axes = InputAdjust(axes);
        SetVelocity(axes * info.velocity);
        transform.localPosition += new Vector3(0, 0.004f * Mathf.Sin(Time.time * 2f), 0);
        p_info.baseSpeed = isTouching ? rb.velocity : Vector2.zero;

        if (input.CloudSkill && skill.skillReady)
            anim.Play("CloudSkill");
    }

    private Vector2 InputAdjust(Vector2 axes)
    {
        if (transform.position.x <= leftEdge + 2 && axes.x < 0)
            axes.x = 0;
        if(transform.position.y <= bottomEdge + 2 && axes.y < 0)
            axes.y = 0;
        if(transform.position.y >= topEdge - 4 && axes.y > 0)
            axes.y = 0;
        return axes;
    }

    //监视玩家是否处在云上
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            isTouching= true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isTouching = false;
    }
}
