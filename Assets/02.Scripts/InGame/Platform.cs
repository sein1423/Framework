using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{
    public bool b_isSteped; //������ �� true��
    public bool b_isTarget; //Ÿ���� �� true��
    public GameObject comboTextPrefab;
    public Transform canvasTransform;

    private void Awake()
    {
        b_isSteped = false;
        b_isTarget = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !b_isSteped && b_isTarget) //�ѹ��� ���� �ʾҰ�, Ÿ���̸�
        {
            GameManager.instance.combo++;
            //�޺� �ؽ�Ʈ ���
            GameObject textObject = Instantiate(comboTextPrefab);
            Vector3 uiPosition = Camera.main.WorldToScreenPoint(transform.position);
            textObject.transform.SetParent(canvasTransform);
            textObject.transform.position = uiPosition;
            //
            b_isSteped = true;
        }

        if (collision.gameObject.CompareTag("Player") && !b_isSteped && !b_isTarget) // �ѹ��� ���� �ʾҰ�, Ÿ���� �ƴϸ�
        {
            GameManager.instance.combo = 0;
            b_isSteped = true;
        }

    }
}
