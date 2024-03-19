using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人被击中时的泛红反馈,由于在Animator里会顶掉别的动画，只能放这了
/// </summary>
public class HittenEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float holdTime = 0.2f;
    public Color HittenColor = Color.red;
    private Color OriginalColor;

    public bool cancel;

    private void Awake()
    {
        if((spriteRenderer = GetComponent<SpriteRenderer>()) == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        OriginalColor = spriteRenderer.color;
    }

    /// <summary>
    /// 入口函数，调用敌人身上该函数来触发效果
    /// </summary>
    public void Hitten()
    {
        StopCoroutine(nameof(ColorChange));
        StartCoroutine(nameof(ColorChange));
    }
    private IEnumerator ColorChange()
    {
        spriteRenderer.color = HittenColor;
        float timer = 0;
        while(timer <= holdTime)
        {
            timer += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(HittenColor, OriginalColor, timer / holdTime);
            yield return null;
        }
    }
}
