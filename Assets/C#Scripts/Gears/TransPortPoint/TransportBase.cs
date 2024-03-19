using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransportBase : Switch
{
    private GameObject point;
    private bool isActive = false;

    public GameObject particle;

    private void Awake()
    {
        point = transform.GetChild(0).gameObject;
    }
    protected override void OnTouch(Collider2D collision)
    {
        base.OnTouch(collision);

        if(collision.tag == "Player")
        {
                Transport trans;
                if (!collision.TryGetComponent(out trans))
                    trans = collision.AddComponent<Transport>();
                trans.OnPointChanged(this);
        }
    }

    public void OnActived()
    {
        if (isActive)
            return;
        ObjectPool.Instance.GetObj(particle, point.transform.position, Quaternion.identity);
        point.SetActive(true);
        isActive = true;
    }

    public void OnCancelled()
    {
        if (!isActive)
            return;
        point.SetActive(false);
        isActive = false;
    }
}
