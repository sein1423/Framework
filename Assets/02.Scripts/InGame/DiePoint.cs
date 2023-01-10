using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player Player = collision.GetComponent<Player>();
            Player.Dead();
        }
    }
}
