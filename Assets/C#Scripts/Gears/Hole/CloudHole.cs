using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHole : MonoBehaviour
{
    public CloudState thisState;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cloud")
        {
            if (collision.TryGetComponent<CloudInfo>(out var info))
                if(info.currentState != thisState)
                    info.ChangeState(thisState);
        }
    }
}
