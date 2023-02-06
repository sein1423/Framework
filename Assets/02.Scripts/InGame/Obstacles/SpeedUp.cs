using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float upSpeed = 1.5f;
    GameManager gameManager;
    UIManager uiManager;
    private void Awake()
    {
        gameManager = GameManager.instance;
        uiManager = UIManager.instance;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            uiManager.ShowSpeedUpGuide();

            if (!gameManager.b_obstacleImmuneItem)
            {
                gameManager.combo = 0;
                Rigidbody2D rigid = collision.GetComponent<Rigidbody2D>();
                rigid.gravityScale = 1.5f;
                gameObject.SetActive(false);
            }

            else
            {
                gameManager.b_obstacleImmuneItem = false;
                gameObject.SetActive(false);
            }
        }
    }
}
