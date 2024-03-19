using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Discriptions : MonoBehaviour
{
    public float speed = 3;
    public bool isEnable;

    private Color origin;
    private Color empty;

    Text t;
    private void Awake()
    {
        t= GetComponent<Text>();
        origin = new Color(t.color.r, t.color.g, t.color.b, 1);
        empty = origin - new Color(0, 0, 0, 1);
        t.color = empty;
    }
    private void Update()
    {
        t.color = Color.Lerp(t.color, isEnable ? origin : empty, isEnable ? Time.deltaTime * speed : Time.deltaTime * speed * 1.5f);
    }
}
