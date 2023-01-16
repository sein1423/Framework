using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    // 경험치 주는 아이템 스크립트
    GameManager gameManager;
    public int expGain = 1;
    public float magneticRange = 2.5f;
    private float magneticSpeed = 10f;
    private void Awake()
    {
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.AddScore(expGain);
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (gameManager.b_magneticItem)
        {
            MagneticToPlayer();
        }
    }

    public void MagneticToPlayer()
    {
        Transform playerTransform = gameManager.GetPlayer.transform;
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        Vector3 difPos = playerTransform.position - transform.position;
        Vector3 playerDir = difPos.normalized;
        playerDir = playerDir * magneticSpeed *Time.deltaTime;
        if(distance <= magneticRange)
        {
            transform.Translate(playerDir);
        }
    }
}
