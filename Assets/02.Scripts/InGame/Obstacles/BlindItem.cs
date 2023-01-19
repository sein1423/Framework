using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindItem : MonoBehaviour
{
    GameManager gameManager;

    private void OnEnable()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!gameManager.b_obstacleImmuneItem)
                TriggerByPlayer();
            else
            {
                gameManager.b_obstacleImmuneItem = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void TriggerByPlayer()
    {
        gameManager.BlindItem_TriggerByPlayer();
        gameObject.SetActive(false);
    }
}
