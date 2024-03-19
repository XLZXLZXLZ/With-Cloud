using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraActions : Singleton<CameraActions>
{
    public float leftEdge, rightEdge, topEdge, bottomEdge;
    public float[] savePoints;
    public float minSize = 18, maxSize = 38;
    public bool focusing = true;

    public GameObject SetFocus;

    public GameObject[] checkItems;
    public Vector2[] checkPoints;

    Camera cam;
    Transform player_pos;//ÕÊº“Œª÷√
    Transform cloud_pos; //‘∆Œª÷√

    private void Awake()
    {
        player_pos = GameObject.FindGameObjectWithTag("Player").transform;
        cloud_pos = GameObject.FindGameObjectWithTag("Cloud").transform;
        cam = Camera.main;
    }

    private void Start()
    {
        transform.position = Vector3.Lerp(player_pos.position, cloud_pos.position, 0.5f);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!focusing)
            return;
        Vector3 focus = SetFocus == null ? Vector3.Lerp(player_pos.position, cloud_pos.position, 0.5f) : SetFocus.transform.position;

        float distance = ((Vector2)(player_pos.position - cloud_pos.position)).magnitude;
        
        float size = Mathf.Clamp(minSize + (distance - minSize) * (maxSize - minSize) / 40, minSize, maxSize);

        focus = EdgeDetective(focus);
        size = SizeDetective(focus,size);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, Time.deltaTime * 3f);

        cam.transform.position = Vector3.Lerp(cam.transform.position, focus + new Vector3(0, 0, -10), Time.fixedDeltaTime * 4f);
    }

    private Vector3 EdgeDetective(Vector3 focus)
    {
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float camLeftEdge = cam.transform.position.x - camWidth / 2f;
        float camRightEdge = cam.transform.position.x + camWidth / 2f ;
        float camTopEdge = cam.transform.position.y + camHeight / 2f;
        float camBottomEdge = cam.transform.position.y - camHeight / 2f;

        if (focus.x < cam.transform.position.x && camLeftEdge <= leftEdge)
            focus.x = cam.transform.position.x;
        else if (focus.x > cam.transform.position.x && camRightEdge >= rightEdge)
            focus.x = cam.transform.position.x;

        if (focus.y > cam.transform.position.y && camTopEdge >= topEdge)
            focus.y = cam.transform.position.y;
        else if (focus.y < cam.transform.position.y && camBottomEdge <= bottomEdge)
            focus.y = cam.transform.position.y;

        return focus;
    }

    private float SizeDetective(Vector3 focus,float size)
    {
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float camLeftEdge = cam.transform.position.x - camWidth / 2f - 1;
        float camRightEdge = cam.transform.position.x + camWidth / 2f + 1;
        float camTopEdge = cam.transform.position.y + camHeight / 2f + 1;
        float camBottomEdge = cam.transform.position.y - camHeight / 2f - 1;

        if (focus.x < cam.transform.position.x && camLeftEdge <= leftEdge)
            size = cam.orthographicSize;
        else if (focus.x > cam.transform.position.x && camRightEdge >= rightEdge)
            size = cam.orthographicSize;

        if (focus.y > cam.transform.position.y && camTopEdge >= topEdge)
            size = cam.orthographicSize;
        else if (focus.y < cam.transform.position.y && camBottomEdge <= bottomEdge)
            size = cam.orthographicSize;

        return size;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftEdge, bottomEdge), new Vector3(rightEdge, bottomEdge));
        Gizmos.DrawLine(new Vector3(leftEdge,topEdge), new Vector3(rightEdge, topEdge));
        Gizmos.DrawLine(new Vector3(leftEdge,topEdge),new Vector3(leftEdge, bottomEdge));

        foreach(var c in checkPoints)
        {
            Gizmos.DrawWireSphere(c, 3);
        }

        Gizmos.color= Color.green;
        Gizmos.DrawLine(new Vector3(rightEdge, topEdge), new Vector3(rightEdge, bottomEdge));

        Gizmos.color = Color.yellow;
        foreach(var s in savePoints)
        {
            Gizmos.DrawLine(new Vector3(s, bottomEdge - 10), new Vector3(s, topEdge + 10));
        }
    }
}
