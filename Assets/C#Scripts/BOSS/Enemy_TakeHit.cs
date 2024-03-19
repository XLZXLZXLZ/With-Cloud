using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人受击脚本,玩家攻击后将数据发送到此处处理
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
        hittenEffect.Hitten();  //打击感反馈
        //CharacterEvent.DamageEvent.Invoke(gameObject, damage);//伤害跳字
    }
}
