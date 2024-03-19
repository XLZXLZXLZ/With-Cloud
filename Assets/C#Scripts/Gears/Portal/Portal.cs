using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Switch
{
    public int index;
    private static bool isUsing;
    protected override void Update()
    {
        if (isTouch && Input.GetKeyDown(KeyCode.W))
            Translate();
    }

    private void Translate()
    {
        if (isUsing)
            return;
        var des = transform.parent.GetChild((index + 1) % transform.parent.childCount);

        StartCoroutine(Translating(Tools.player.transform, des.position));
    }

    private IEnumerator Translating(Transform player,Vector2 des)
    {
        isUsing = true;
        yield return null;
        player.position = des;
        isUsing= false; 
    }
}
