using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloudDeath : MonoBehaviour
{
    public GameObject deathPatricle;
    private bool isDie;
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
        isDie = true;
        
        ObjectPool.Instance.GetObj(deathPatricle, transform.position, Quaternion.identity);
        transform.position = new Vector3(0,0,-100);

        foreach (var sr in GetComponentsInChildren<SpriteRenderer>(transform))
            sr.color -= new Color(0, 0, 0, 1);

        CameraActions.Instance.focusing = false;

        yield return new WaitForSeconds(1);
        Cover.Instance.ChangeScene(SceneManager.GetActiveScene().name, 1f);
    }
}
