using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{
    public bool b_isSteped; //밟혓을 때 true로
    public bool b_isTarget; //타겟일 때 true로
    public GameObject comboTextPrefab;
    public Transform canvasTransform;

    private void Awake()
    {
        b_isSteped = false;
        b_isTarget = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !b_isSteped && b_isTarget) //한번도 밟지 않았고, 타겟이면
        {
            GameManager.instance.combo++;
            //콤보 텍스트 출력
            GameObject textObject = Instantiate(comboTextPrefab);
            Vector3 uiPosition = Camera.main.WorldToScreenPoint(transform.position);
            textObject.transform.SetParent(canvasTransform);
            textObject.transform.position = uiPosition;
            //
            b_isSteped = true;
        }

        if (collision.gameObject.CompareTag("Player") && !b_isSteped && !b_isTarget) // 한번도 밟지 않았고, 타겟이 아니면
        {
            GameManager.instance.combo = 0;
            b_isSteped = true;
        }

    }
}
