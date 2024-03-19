using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchDetective : MonoBehaviour
{
    private Collider2D coll;
    //该角色是否触碰了地板/平台及是否卡在平台内
    public bool isGround =>
        Physics2D.BoxCast(coll.bounds.center, groundBoxCastSize, 0f, Vector2.down, groundBoxCastDistance, Tools.GetLayer("Ground")).collider != null ||
        Physics2D.BoxCast(coll.bounds.center, groundBoxCastSize, 0f, Vector2.down, groundBoxCastDistance, Tools.GetLayer("Cloud")).collider != null;
    //该角色是否触碰了墙体
    public bool isWall => false; //Physics2D.BoxCast(coll.bounds.center, wallBoxCastSize, 0f, Vector2.right, wallBoxCastDistance * transform.localScale.x, Tools.GetLayer("Ground")).collider != null;


    [Header("探测包围盒设置")]
    public Vector2 groundBoxCastSize = new Vector2(1f, 0.1f);  // 地面包围盒的大小
    public float groundBoxCastDistance = 0.1f;  // 地面包围盒的投射距离
    public Vector2 wallBoxCastSize = new Vector2(0.1f, 1f); //墙体包围盒大小
    public float wallBoxCastDistance = 0.1f;    //墙体包围盒投射距离

    [Header("预测包围盒设置")]
    public Vector2 forcastBoxCastSize = new Vector2(1f, 0.1f);  // 地面包围盒的大小
    public float forcastBoxCastDistanceX = 0.1f;    // 地面包围盒的水平投射距离
    public float forcastBoxCastDistanceY = 0.1f;    // 地面包围盒的垂直投射距离
    private Vector2 forcastBoxDirection;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        forcastBoxDirection = new Vector2(forcastBoxCastDistanceX, forcastBoxCastDistanceY);
    }

    private void OnDrawGizmosSelected()
    {
    #if UNITY_EDITOR
        // 绘制包围盒,可视化编辑
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + Vector3.down * groundBoxCastDistance, groundBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + Vector3.right * wallBoxCastDistance * transform.localScale.x, wallBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + new Vector3(forcastBoxCastDistanceX * transform.localScale.x, forcastBoxCastDistanceY), forcastBoxCastSize);
    #endif
    }

}
