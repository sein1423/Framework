using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BounceOff : MonoBehaviour
{
    GameManager gameManager;
    int deExp = 0;
    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            deExp = -gameManager.ExpTotal / 5;
            gameManager.OnCollisionByObstacle(this, deExp);
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
