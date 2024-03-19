using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchDetective : MonoBehaviour
{
    private Collider2D coll;
    //�ý�ɫ�Ƿ����˵ذ�/ƽ̨���Ƿ���ƽ̨��
    public bool isGround =>
        Physics2D.BoxCast(coll.bounds.center, groundBoxCastSize, 0f, Vector2.down, groundBoxCastDistance, Tools.GetLayer("Ground")).collider != null ||
        Physics2D.BoxCast(coll.bounds.center, groundBoxCastSize, 0f, Vector2.down, groundBoxCastDistance, Tools.GetLayer("Cloud")).collider != null;
    //�ý�ɫ�Ƿ�����ǽ��
    public bool isWall => false; //Physics2D.BoxCast(coll.bounds.center, wallBoxCastSize, 0f, Vector2.right, wallBoxCastDistance * transform.localScale.x, Tools.GetLayer("Ground")).collider != null;


    [Header("̽���Χ������")]
    public Vector2 groundBoxCastSize = new Vector2(1f, 0.1f);  // �����Χ�еĴ�С
    public float groundBoxCastDistance = 0.1f;  // �����Χ�е�Ͷ�����
    public Vector2 wallBoxCastSize = new Vector2(0.1f, 1f); //ǽ���Χ�д�С
    public float wallBoxCastDistance = 0.1f;    //ǽ���Χ��Ͷ�����

    [Header("Ԥ���Χ������")]
    public Vector2 forcastBoxCastSize = new Vector2(1f, 0.1f);  // �����Χ�еĴ�С
    public float forcastBoxCastDistanceX = 0.1f;    // �����Χ�е�ˮƽͶ�����
    public float forcastBoxCastDistanceY = 0.1f;    // �����Χ�еĴ�ֱͶ�����
    private Vector2 forcastBoxDirection;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        forcastBoxDirection = new Vector2(forcastBoxCastDistanceX, forcastBoxCastDistanceY);
    }

    private void OnDrawGizmosSelected()
    {
    #if UNITY_EDITOR
        // ���ư�Χ��,���ӻ��༭
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + Vector3.down * groundBoxCastDistance, groundBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + Vector3.right * wallBoxCastDistance * transform.localScale.x, wallBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + new Vector3(forcastBoxCastDistanceX * transform.localScale.x, forcastBoxCastDistanceY), forcastBoxCastSize);
    #endif
    }

}
