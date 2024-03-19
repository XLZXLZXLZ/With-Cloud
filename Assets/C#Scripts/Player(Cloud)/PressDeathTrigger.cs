using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressDeathTrigger : MonoBehaviour
{
    private CloudDeath death;
    private PlayerDeath p_death;

    private void Awake()
    {
        death = GetComponentInParent<CloudDeath>();
        p_death = GetComponentInParent<PlayerDeath>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if(death != null)
                death.Death();
            else if(p_death != null)
                p_death.Death();    
        }
    }
}
