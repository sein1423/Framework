using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverBoard : MonoBehaviour
{
    public float downSpeed = 1;
    public float playerSpeed = 2;
    public Transform startTransform;
    private bool moving;
    private float xPos;
    private Vector3 originPos;
    private Vector3 mousePos;
    private Vector3 offset;

    private void Awake()
    {
        originPos = transform.position;
    }
    private void Update()
    {
        if (GameManager.instance.b_startFever) //피버 시작 시 맵이 위로 올라감, 드래그로 플레이어 움직이기 가능
        {
            transform.Translate(Vector3.up * Time.deltaTime * downSpeed);
            MovePlayerByDrag();
        }

        if(!GameManager.instance.b_startFever) //피버가 끝나면 원래 위치로 돌아감
        {
            transform.position = originPos;
        }
    }

    private void OnMouseDown()
    {
        moving = true;
        mousePos = Input.mousePosition;

    }

    private void OnMouseUp()
    {
        moving = false;
    }
    /// <summary>
    /// 드래그로 플레이어 이동
    /// </summary>
    private void MovePlayerByDrag()
    {
        if (moving)
        {
            offset = (Input.mousePosition - mousePos);
            xPos += (offset.x) * Time.deltaTime * playerSpeed;
            xPos = Mathf.Clamp(xPos, -3f, 3f);
            Player.instance.gameObject.transform.position = new Vector3(startTransform.position.x + xPos, startTransform.position.y, startTransform.position.z);
            mousePos = Input.mousePosition;
        }
    }
}
