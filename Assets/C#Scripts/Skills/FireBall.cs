using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject particle;
    public float speed = 60;
    public void Shoot(Vector2 dir)
    {
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ObjectPool.Instance.GetObj(particle, transform.position, Quaternion.identity);
    }
}
