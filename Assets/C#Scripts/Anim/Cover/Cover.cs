using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cover : Singleton<Cover>
{
    Image i;

    private void Awake() 
    {
        GenerateCover();
    }

    private void GenerateCover()
    {
        var c = this.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.sortingOrder = 100;

        var go = new GameObject();
        go.transform.SetParent(this.transform, false);

        i = go.AddComponent<Image>();
        i.color = Color.black;
        i.transform.localScale = new Vector3(10000, 10000);
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        ChangeScene(sceneName, 1f);
    }
    public void ChangeScene(string sceneName,float time)
    {
        if (!isChanging)
            StartCoroutine(ChangingScene(sceneName,time/2));
    }


    private bool isChanging;
    private IEnumerator ChangingScene(string sceneName,float time)
    {
        isChanging = true;
        i.color -= new Color(0, 0, 0, 1f);
        while (i.color.a < 1)
        {
            i.color += new Color(0, 0, 0, Time.deltaTime/time);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        yield return null;
        while (i.color.a > 0)
        {
            i.color -= new Color(0, 0, 0, time * Time.deltaTime/time);
            yield return null;
        }
        isChanging = false;
    }
}
