using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGoal : MonoBehaviour
{
    private AudioSource goalSound;
    GameManager gameManager;

    private void Awake()
    {
        goalSound = GetComponent<AudioSource>();
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            goalSound.Play();
            gameManager.Success();           
        }
    }
}
