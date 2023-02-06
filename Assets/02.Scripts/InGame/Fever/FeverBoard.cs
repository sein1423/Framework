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
    Player player;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        originPos = transform.position;
        player = gameManager.Player;
    }
    private void Update()
    {
        if (gameManager.b_StartFever) //�ǹ� ���� �� ���� ���� �ö�, �巡�׷� �÷��̾� �����̱� ����
        {
            transform.Translate(Vector3.up * Time.deltaTime * downSpeed);
            MovePlayerByDrag();
        }

        if(!gameManager.b_StartFever) //�ǹ��� ������ ���� ��ġ�� ���ư�
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
            player.transform.position = new Vector3(startTransform.position.x + xPos, startTransform.position.y, startTransform.position.z);
            mousePos = Input.mousePosition;
        }
    }
}
