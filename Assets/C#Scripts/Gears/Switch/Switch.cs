using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SwitchAction();
public class Switch : MonoBehaviour
{
    public SwitchAction switchOn;
    public SwitchAction switchOff;
    public bool isOn;

    protected bool isTouch;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isTouch)
            InteractAction();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouch = true;
            OnTouch(collision);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouch = false;
            OnExit(collision);
        }
    }

    protected virtual void InteractAction()
    {

    }

    protected virtual void OnTouch(Collider2D collision)
    {

    }

    protected virtual void OnExit(Collider2D collision)
    {

    }
}
