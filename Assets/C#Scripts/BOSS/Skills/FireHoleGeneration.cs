using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class FireHoleGeneration : MonoBehaviour
{
    public GameObject fireHole;
    public float range = 10;

    private void Awake()
    {
        Generation();
    }
    public void Generation()
    {
        StartCoroutine(Generating());
    }

    private IEnumerator Generating()
    {
        var p = Instantiate(fireHole, (Vector2)transform.position + Random.insideUnitCircle * range, Quaternion.identity).GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(15);
        var emission = p.emission;
        emission.rateOverTime = 0;
        p.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(p.main.startLifetime.constantMax * 2);
        Destroy(p.gameObject);
        yield return Generating();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
