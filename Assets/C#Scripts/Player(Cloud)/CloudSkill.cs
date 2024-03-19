using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CloudSkill : MonoBehaviour
{
    CloudInfo info;
    CloudController controller;
    PlayerInfo p_info;
    Rigidbody2D rb;

    private float skillCalm;

    public Vector2 boxSize;
    public float castDistance = 1.0f;
    public GameObject blueSkillParticle;
    public GameObject redSkillParticle;
    public GameObject fireBall;

    public float blowHeight = 30;

    public bool skillReady => ((info.currentState == CloudState.Red && controller.isTouching && p_info.holdingFire > 0) || info.currentState == CloudState.Blue) && skillCalm < 0;

    private void Awake()
    {
        p_info = PlayerInfo.Instance;
        info = GetComponent<CloudInfo>();
        controller = GetComponent<CloudController>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        skillCalm -= Time.deltaTime;
    }
    public bool SkillTrigger()
    {
        if (info.currentState != CloudState.Red && info.currentState != CloudState.Blue)
            return false;

        //蓝云技能:喷射
        if(info.currentState == CloudState.Blue)
        {
            ObjectPool.Instance.GetObj(blueSkillParticle, transform.position, Quaternion.identity);
            Vector2 center = transform.position + new Vector3(0, castDistance, 0);
            Collider2D collider = Physics2D.OverlapBox(center, boxSize, 0, Tools.GetLayer("Player"));
            if (collider?.gameObject.tag == "Player")
            {
                var r = collider.GetComponent<Rigidbody2D>();
                r.velocity = new Vector2(r.velocity.x, blowHeight);
                collider.GetComponent<PlayerControl>().airJumpChance = 1;
            }
            skillCalm = 0.5f;
        }

        //红云技能:喷火
        if(info.currentState == CloudState.Red && p_info.holdingFire > 0)
        {
            var p = ObjectPool.Instance.GetObj(redSkillParticle, transform.position + new Vector3(transform.localScale.x * 2,0,0), Quaternion.Euler(0,0,(90 * (transform.localScale.x - 1)))); //粒子效果

            var ball = Instantiate(fireBall, transform.position, Quaternion.identity).GetComponent<FireBall>(); //火球
            ball.Shoot(new Vector2(transform.localScale.x,0));

            rb.velocity = new Vector2(-transform.localScale.x * 10, 0); //后坐力

            skillCalm = 0.25f;
            p_info.holdingFire--;
        }

        
        return true;
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 center = transform.position + new Vector3(0, castDistance,0);
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(center, new Vector3(boxSize.x, boxSize.y, 1));
    }
}
