using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeDown : MonoBehaviour
{
    private Vector3 downSize;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.instance;
        downSize = new Vector3(0.25f, 0.25f, 0.25f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!gameManager.b_obstacleImmuneItem)
            {
                Player.instance.SetSize(downSize);
                gameManager.combo = 0;
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
