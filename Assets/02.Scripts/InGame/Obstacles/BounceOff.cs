using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOff : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!gameManager.b_obstacleImmuneItem)
            {
                gameManager.expTotal -= gameManager.expTotal / 5;
                gameManager.combo = 0;
                UIManager.instance.ShowDamagedScore();
            }

            else
            {
                gameManager.b_obstacleImmuneItem = false;
                gameObject.SetActive(false);
            }
        }
    }
}
