using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    private Text comboText;
    GameManager gameManager;
    Color alpha;
    private void Start()
    {
        gameManager = GameManager.instance;
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        comboText = GetComponent<Text>();
        alpha = comboText.color;
        comboText.text = gameManager.combo + " Combo!";

        Invoke("DestroyObject", destroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); //���� ���� ���������
        comboText.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
