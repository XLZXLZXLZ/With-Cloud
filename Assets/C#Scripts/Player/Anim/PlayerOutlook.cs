using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutlook : MonoBehaviour
{
    private Color origin;
    private SpriteRenderer sr;
    private Color targetColor;
    private PlayerInfo info;

    public Color[] fireColors;
    [HideInInspector]
    public bool isDie = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        info = PlayerInfo.Instance;
        origin = sr.color;
        targetColor = origin;
    }

    private void Update()
    {
        if (isDie)
            return;
        sr.color = Color.Lerp(sr.color, targetColor, Time.deltaTime * 2f);
        targetColor = info.holdingFire > 0 ? fireColors[fireColors.Length - info.holdingFire] : origin;
    }


}
