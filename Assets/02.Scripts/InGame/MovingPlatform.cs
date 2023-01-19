using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveRange;
    public float moveSpeed;
    public float rotateSpeed;
    public bool dirX, dirY, rotate;
    private Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
    }
    private void Update()
    {
        //if(GameManager.instance.b_gameStart)
        //{
        Vector3 v = pos;

        if (dirX)
        {
            v.x -= moveRange * Mathf.Sin(moveSpeed * Time.time);
            transform.position = v;
        }
        if (dirY)
        {
            v.y += moveRange * Mathf.Sin(moveSpeed * Time.time);
            transform.position = v;
        }

        if(rotate)
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        }
        
        //}
    }
}
