using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMove : MonoBehaviour
{
    public Vector2 pos1, pos2;
    public float speed = 1;

    private void Start()
    {
        pos1 = (Vector2)transform.position + pos1;
        pos2 = (Vector2)transform.position + pos2;
    }

    private void FixedUpdate()
    {
        float value = (Mathf.Sin(Time.time* speed) + 1)/2;
        transform.position = Vector2.Lerp(pos1,pos2,value);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine((Vector2)transform.position + pos1,(Vector2)transform.position + pos2);
        Gizmos.DrawWireSphere((Vector2)transform.position + pos1, 1);
        Gizmos.DrawWireSphere((Vector2)transform.position + pos2, 1);
    }
}
