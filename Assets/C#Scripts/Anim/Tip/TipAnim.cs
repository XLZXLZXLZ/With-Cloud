using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipAnim : MonoBehaviour
{
    Text t;
    Transform p;
    public bool player = true;

    public float distance;
    bool isAppear => (((p.position - transform.position).magnitude < distance) && player);

    private void Awake()
    {
        t = GetComponent<Text>();
        p = Tools.player.transform;
    }

    private void Update()
    {
        if (isAppear && t.color.a < 1)
            t.color += new Color(0, 0, 0, Time.deltaTime);
        else if (!isAppear && t.color.a > 0)
            t.color -= new Color(0, 0, 0, Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
