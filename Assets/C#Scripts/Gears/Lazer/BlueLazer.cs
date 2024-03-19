using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLazer : MonoBehaviour
{
    LineRenderer line;

    public float maxDistance;
    public GameObject targetSwitch;
    public GameObject lazerParticle;

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
        line.startWidth = Mathf.MoveTowards(line.startWidth, lineWidth * width, Time.deltaTime * 2);

        if (!isOn)
            return;
        RaycastHit2D target;
        target = Physics2D.Raycast(transform.position, transform.up, maxDistance, Tools.GetLayer("Ground"));

        Vector2 des;
        if (!target)
            des = transform.position + transform.up * maxDistance;
        else if (target)
        {
            float length = ((Vector3)target.point - transform.position).magnitude;
            if (length < lastLength)
                des = target.point;
            else
                des = Vector3.Lerp(destination.transform.position, target.point, 180 * Time.deltaTime);
        }
        else
            des = Vector2.zero;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, des);
        destination.position = des;

        RaycastHit2D cloud = Physics2D.Linecast(transform.position, des, Tools.GetLayer("Cloud"));
        if (cloud && !isCalm)
        {
            cloud.rigidbody.velocity = -cloud.rigidbody.velocity.normalized * 15;
            StartCoroutine(LazerCalm());
            ObjectPool.Instance.GetObj(lazerParticle, cloud.transform.position + Vector3.right * 3.5f * cloud.transform.localScale.x, Quaternion.identity);
        }
        lastLength = (destination.transform.position - transform.position).magnitude;

    }

    private IEnumerator LazerCalm()
    {
        isCalm = true;
        yield return new WaitForSeconds(0.3f);
        isCalm = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * maxDistance);

        if (targetSwitch != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetSwitch.transform.position);
        }
    }

    public void SwitchOn()
    {
        destination.gameObject.SetActive(false);
        width = 0;
        isOn = false;
    }

    public void SwitchOff()
    {
        destination.gameObject.SetActive(true);
        width = 1;
        isOn = true;
    }
}
