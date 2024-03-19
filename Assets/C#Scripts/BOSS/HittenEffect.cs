using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˱�����ʱ�ķ��췴��,������Animator��ᶥ����Ķ�����ֻ�ܷ�����
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
    /// ��ں��������õ������ϸú���������Ч��
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
