using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugger : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            CameraActions.Instance.SetFocus = Tools.player;
        }
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            CameraActions.Instance.SetFocus = GameObject.FindGameObjectWithTag("Cloud");
        }
        if(Input.GetKeyDown(KeyCode.H)) 
        {
            BGMManager.Instance.StopBGM(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject);
    }
}
