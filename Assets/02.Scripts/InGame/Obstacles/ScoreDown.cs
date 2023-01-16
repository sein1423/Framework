using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDown : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        int grade = Player.instance.grade;
        switch(grade)
        {
            case 0:
                spriteRenderer.color = Color.white;
                break;
            case 1:
                spriteRenderer.color = Color.green;
                break;
            case 2:
                spriteRenderer.color = Color.cyan;
                break;
            case 3:
                spriteRenderer.color = Color.blue;
                break;
            case 4:
                spriteRenderer.color = Color.red;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!gameManager.b_obstacleImmuneItem)
            {
                Player.instance.Damage();
                GameManager.instance.combo = 0;
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
