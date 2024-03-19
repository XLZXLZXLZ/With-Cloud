using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entry : MonoBehaviour
{
    private Image c;
    private float timer = 0;
    private void Awake()
    {
        c = transform.Find("COVER").GetComponent<Image>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < 0.75)
        {
            c.color -= new Color(0, 0, 0, Time.deltaTime/0.75f);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Cover.Instance.ChangeScene("MainMenu",2);
    }
}
