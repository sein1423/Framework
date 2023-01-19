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
                Player.instance.GravityUp(upSpeed);
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
