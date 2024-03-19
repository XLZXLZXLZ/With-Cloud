using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle_Feather : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //´¥ÅöÍæ¼Ò£¬±¬Õ¨
        {
            other.GetComponent<PlayerDeath>().Death();
        }
    }
}
