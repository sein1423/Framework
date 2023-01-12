using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGoal : MonoBehaviour
{
    private AudioSource goalSound;

    private void Awake()
    {
        goalSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            goalSound.Play();

            Player Player = collision.gameObject.GetComponent<Player>();
            GameManager.instance.AddScore();
            Player.Dead();
            
        }
    }
}
