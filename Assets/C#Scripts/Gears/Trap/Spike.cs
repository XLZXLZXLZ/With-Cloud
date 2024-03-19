using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public bool killCloud = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerDeath>().Death();
        }
        if(collision.gameObject.tag == "Cloud" && killCloud)
        {
            collision.GetComponent<CloudDeath>().Death();
        }
    }
}
