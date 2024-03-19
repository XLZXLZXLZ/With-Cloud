using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public GameObject targetSwitch;

    public GameObject[] BOSSItems;

    private Switch target;
    private void Awake()
    {
        if (targetSwitch == null)
            return;
        targetSwitch.TryGetComponent(out target);
        target.switchOn += SwitchOn;
        target.switchOff += SwitchOff;
    }

    private void Start()
    {
        BGMManager.Instance.StopBGM(2);
    }

    protected virtual void SwitchOn()
    {
        foreach(var g in BOSSItems)
        {
            g.SetActive(true);
        }
        BGMManager.Instance.PlayBGM("BOSS", 2, 0.7f);
    }

    protected virtual void SwitchOff()
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (targetSwitch != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetSwitch.transform.position, 0.5f);
            Gizmos.DrawLine(transform.position, targetSwitch.transform.position);
        }
    }
}
