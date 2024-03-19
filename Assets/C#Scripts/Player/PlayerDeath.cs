using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathPatricle, r_deathParticle;
    private bool isDie;
    private float leftEdge, rightEdge, topEdge, bottomEdge;

    private void Start()
    {
        leftEdge = CameraActions.Instance.leftEdge;
        rightEdge = CameraActions.Instance.rightEdge;
        topEdge = CameraActions.Instance.topEdge;
        bottomEdge = CameraActions.Instance.bottomEdge;
    }
    public void Death()
    {
        if (isDie)
            return;
        if (SceneManager.GetActiveScene().name != "MainMenu")
            CheckPoints.Instance.UpdateIndex(transform);
        StartCoroutine(DeathAnim());
    }

    private IEnumerator DeathAnim()
    {
        isDie= true;

        ObjectPool.Instance.GetObj(PlayerInfo.Instance.holdingFire > 0 ? r_deathParticle : deathPatricle, transform.position, Quaternion.identity);
        transform.position = Vector3.zero;

        foreach (var sr in GetComponentsInChildren<SpriteRenderer>(transform))
            sr.color -= new Color(0, 0, 0, 1);
        GetComponentInChildren<PlayerOutlook>().isDie = true;

        CameraActions.Instance.focusing = false;

        yield return new WaitForSeconds(1);
        Cover.Instance.ChangeScene(SceneManager.GetActiveScene().name, 1f);
    }

    private void Update()
    {
        if (transform.position.y > topEdge + 2 || transform.position.y < bottomEdge - 2)
            Death();
    }
}
