using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonActions : MonoBehaviour
{
    public GameObject burstParticle;
    private void OnDisable()
    {
        ObjectPool.Instance.GetObj(burstParticle, transform.position - new Vector3(0, 1, 0), Quaternion.Euler(0, transform.parent.localScale.x > 0 ? 0 : 180, 0));
    }
}
