using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private Transform headers, tips, arrow, clouds;
    private int currentIndex;

    private void Awake()
    {
        headers = transform.Find("Canvas").Find("Headers");
        tips = transform.Find("Canvas").Find("Tips");
        arrow = transform.Find("Arrow");
        clouds = transform.Find("Clouds");

    }
    private void Start()
    {
        if (GlobalGameManager.UnlockLevel >= 2)
            Tools.player.transform.position = GameObject.FindGameObjectWithTag("Cloud").transform.position + Vector3.up * 3;
        if (GlobalGameManager.UnlockLevel >= 6)
            Tools.player.GetComponent<PlayerInfo>().flyAbility = true;

        string bgm = GlobalGameManager.UnlockLevel >= 6 ? "BGM1" : "Main";
        if (BGMManager.Instance.currentBGM != bgm)
            BGMManager.Instance.SwitchBGM(bgm, 1, 1);
        for (int i = 9; i >= GlobalGameManager.UnlockLevel; i--)
            clouds.GetChild(i).gameObject.SetActive(false);
    }

    public void SwitchChoice(int index)
    {
        currentIndex = index;
        arrow.GetComponent<ArrowActions>().index= index;
        tips.GetChild(currentIndex).GetComponent<Discriptions>().isEnable= true;
        headers.GetChild(currentIndex).GetComponent<Discriptions>().isEnable = true;
    }
    
    public void OnExit()
    {
        tips.GetChild(currentIndex).GetComponent<Discriptions>().isEnable = false;
        headers.GetChild(currentIndex).GetComponent<Discriptions>().isEnable = false;
    }

    private void Update()
    {

    }
}
