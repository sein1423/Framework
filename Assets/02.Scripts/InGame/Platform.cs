using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{
    public bool b_isSteped; //������ �� true��
    public bool b_isTarget; //Ÿ���� �� true��
    public GameObject comboTextPrefab;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
        b_isSteped = false;
        b_isTarget = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !b_isSteped)
        {
            if (b_isTarget) //�ѹ��� ���� �ʾҰ�, Ÿ���̸�
            {
                gameManager.combo++;
                //�޺� �ؽ�Ʈ ���
                GameObject textObject = Instantiate(comboTextPrefab);
                Vector3 uiPosition = Camera.main.WorldToScreenPoint(transform.position);
                textObject.transform.SetParent(gameManager.canvasTransform);
                textObject.transform.position = uiPosition;
                //
                b_isSteped = true;
            }
            else // �ѹ��� ���� �ʾҰ�, Ÿ���� �ƴϸ�
            {
                gameManager.combo = 0;
                b_isSteped = true;
            }
        }
       

    }
}
