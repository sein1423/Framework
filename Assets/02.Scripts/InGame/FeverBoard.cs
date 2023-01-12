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
        if (GameManager.instance.b_startFever) //�ǹ� ���� �� ���� ���� �ö�, �巡�׷� �÷��̾� �����̱� ����
        {
            transform.Translate(Vector3.up * Time.deltaTime * downSpeed);
            MovePlayerByDrag();
        }

        if(!GameManager.instance.b_startFever) //�ǹ��� ������ ���� ��ġ�� ���ư�
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
    /// �巡�׷� �÷��̾� �̵�
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
