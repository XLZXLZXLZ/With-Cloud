using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Angel : MonoBehaviour
{
    public int maxHealth = 10;
    private int health;

    [Header("技能组")]
    public GameObject[] SingleLazer;
    public GameObject DoubleLazer;
    public GameObject[] RotateLazer;
    public GameObject DoubleRotateLazer;
    public GameObject Feathers;
    public GameObject HighFeathers;

    [Header("其他准备")]
    public GameObject SceneLazer;  //场景中的激光
    public GameObject EndSwitch;
    public GameObject FlashParticle;
    public GameObject DeathParticle;
    public GameObject DisappearParticle;
    public float topEdge;
    public float bottomEdge;


    private Animator anim;
    private BlueLazer lazer;
    private GameObject cloud;
    private HittenEffect hitten;

    private int hitCount;

    private bool isDie;
    private int lastChoice;

    private GameObject currentSkill;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        lazer = SceneLazer.GetComponent<BlueLazer>();
        hitten = GetComponent<HittenEffect>();

        health = maxHealth;
        topEdge = transform.position.y + topEdge;
        bottomEdge = transform.position.y + bottomEdge;
        cloud = GameObject.FindGameObjectWithTag("Cloud");
    }

    private void Start()
    {
        lazer.SwitchOn();
        ObjectPool.Instance.GetObj(DisappearParticle, transform.position, Quaternion.identity);
        StartCoroutine(StartDelay());
    }

    private void Skill()
    {
        if (isDie)
            return;

        Flash();
        anim.Play("Skill");
        int choice;
        do
        {
            choice = Random.Range(0, 4);
        } while (choice == lastChoice);
        switch (choice)
        {
            case 0:
                if((float)health/maxHealth >= 0.5f)
                    StartCoroutine(SingleLazerSkill());
                else
                    StartCoroutine(DoubleLazerSkill());
                break;
            case 1:
                if ((float)health / maxHealth >= 0.5f)
                    StartCoroutine(RotateLazerSkill());
                else
                    StartCoroutine(DoubleRotateLazerSkill());
                break;
            default:
                if (cloud.transform.position.y > lazer.transform.position.y - 1)
                {
                    Skill();
                    return;
                }
                StartCoroutine(FeatherSkill((float)health / maxHealth >= 0.5f ? Feathers:HighFeathers));
                break;
        }
        lastChoice= choice;
    }

    private IEnumerator SingleLazerSkill()
    {
        currentSkill = Instantiate(SingleLazer[Random.Range(0,2)]);
        yield return new WaitForSeconds(20);
        Skill();
    }

    private IEnumerator DoubleLazerSkill()
    {
        currentSkill = Instantiate(DoubleLazer);
        yield return new WaitForSeconds(20);
        Skill();
    }

    private IEnumerator RotateLazerSkill()
    {
        currentSkill = Instantiate(RotateLazer[Random.Range(0, 2)]);
        yield return new WaitForSeconds(18);
        Skill();
    }

    private IEnumerator DoubleRotateLazerSkill() 
    {
        currentSkill = Instantiate(DoubleRotateLazer);
        yield return new WaitForSeconds(18);
        Skill();
    }

    private IEnumerator FeatherSkill(GameObject skill)
    {
        lazer.SwitchOff();
        currentSkill = Instantiate(skill);
        yield return new WaitForSeconds(15);
        lazer.SwitchOn();
        Skill();
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(3f);
        Skill();
    }

    private void Flash()
    {
        var des = Random.Range(topEdge, bottomEdge);
        if(Mathf.Abs(des - transform.position.y) < (topEdge - bottomEdge)/4)
        {
            Flash();
            return;
        }    

        ObjectPool.Instance.GetObj(FlashParticle, transform.position, Quaternion.identity);
        transform.position = new Vector3(transform.position.x, des, 0);
        ObjectPool.Instance.GetObj(FlashParticle, transform.position, Quaternion.identity);

        hitCount = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x,topEdge,0),new Vector3(transform.position.x,bottomEdge,0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<FireBall>(out var f))
        {
            if (isDie)
                return;
            TakeHit();
            Destroy(collision.gameObject);
        }
    }

    private void TakeHit()
    {
        if (health > 1)
            health--;
        else
            StartCoroutine(DeathAnim());
        hitten.Hitten();

        if (++hitCount >= 3)
            Flash();
    }

    private IEnumerator DeathAnim()
    {
        anim.Play("Die");
        Destroy(currentSkill);
        AttackSense.Instance.CameraShake(5.5f, 0.2f);
        isDie = true;
        BGMManager.Instance.SwitchBGM("Main", 5, 1);
        WaitForSeconds delay = new WaitForSeconds(0.05f);
        for (int i = 0; i < 100; i++)
        {
            ObjectPool.Instance.GetObj(DeathParticle, transform.position + (Vector3)Random.insideUnitCircle * 30, Quaternion.identity);
            yield return delay;
        }
        yield return new WaitForSeconds(0.5f);
        ObjectPool.Instance.GetObj(DisappearParticle, transform.position, Quaternion.identity);
        EndSwitch.GetComponent<GearSwitch>().switchOn();
        Destroy(lazer.gameObject);
        Destroy(gameObject);
    }

}
