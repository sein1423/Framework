using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    // 경험치 주는 아이템 스크립트
    Player player;
    public int expGain = 1;
    public float magneticRange = 2.5f;
    private float magneticSpeed = 10f;
    private int grade;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        player = gameManager.Player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameManager.AddScore(expGain);
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (gameManager.b_magneticItem)
        {
            magneticRange = 5f;
            MagneticToPlayer();
        }

        else
        {
            grade = gameManager.Player.grade;
            
            switch (grade)
            {
                case 0:
                    magneticRange = 0;
                    break;
                case 1:
                    magneticRange = 0.5f;
                    break;
                case 2:
                    magneticRange = 1.5f;
                    break;
                case 3:
                    magneticRange = 2.0f;
                    break;
                case 4:
                    magneticRange = 3.0f;
                    break;
            }
            MagneticToPlayer();
        }
    }

    public void MagneticToPlayer()
    {
        if (player.IsDead)
            return;

        Transform playerTransform = player.transform;
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        Vector3 difPos = playerTransform.position - transform.position;
        Vector3 playerDir = difPos.normalized;
        playerDir = playerDir * magneticSpeed * Time.deltaTime;
        if (distance <= magneticRange)
        {
            transform.Translate(playerDir);
        }

    }
}
