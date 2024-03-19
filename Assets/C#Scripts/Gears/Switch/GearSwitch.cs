using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GearSwitch : Switch
{
    public Sprite onSprite;
    public Sprite offSprite;

    public bool insist = false;

    private SpriteRenderer sr;

    Animator anim;
    private void Awake()
    {
        //anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void OnTouch(Collider2D collision)
    {
        base.OnTouch(collision);
        if (!isOn) 
        {
            isOn = true;
            sr.sprite = onSprite;
            switchOn();
        }
    }

    protected override void OnExit(Collider2D collision)
    {
        base.OnExit(collision);
        if(isOn && !insist)
        {
            isOn = false;
            sr.sprite = offSprite;
            switchOff();
        }
    }
}
