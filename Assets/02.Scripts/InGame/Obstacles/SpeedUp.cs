using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float upSpeed = 1.5f;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!gameManager.b_obstacleImmuneItem)
            {
                GameManager.instance.combo = 0;
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
