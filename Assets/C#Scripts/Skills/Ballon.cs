using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public GameObject particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ObjectPool.Instance.GetObj(particle, transform.position, Quaternion.identity);
            PlayerInfo.Instance.flyAbility = true;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.localPosition += new Vector3(0, 0.008f * Mathf.Sin(Time.time * 1.5f), 0);
    }
}
