using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ܻ��ű�,��ҹ��������ݷ��͵��˴�����
/// </summary>
public class Enemy_TakeHit : MonoBehaviour
{
    private EnemyInfo info;
    private HittenEffect hittenEffect;

    private void Awake()
    {        info = GetComponent<EnemyInfo>();
        hittenEffect = GetComponent<HittenEffect>();
    }

    public void TakeHit(Vector2 dir,float damage)
    {  
        float x = Mathf.Sign(dir.x);
        hittenEffect.Hitten();  //����з���
        //CharacterEvent.DamageEvent.Invoke(gameObject, damage);//�˺�����
    }
}
