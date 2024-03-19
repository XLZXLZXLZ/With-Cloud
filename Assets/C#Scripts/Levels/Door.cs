using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Switch
{
    public int level;
    public bool quit;
    private MainMenuManager main;

    private void Awake()
    {
        main = GetComponentInParent<MainMenuManager>();
    }

    protected override void InteractAction()
    {
        base.InteractAction();

        if(!quit)
            Cover.Instance.ChangeScene("Level" + level.ToString());
        else
            Application.Quit();
    }

    protected override void OnTouch(Collider2D collision)
    {
        base.OnTouch(collision);
        main.SwitchChoice(level - 1);
    }

    protected override void OnExit(Collider2D collision)
    {
        base.OnExit(collision);
        main.OnExit();
    }
}
