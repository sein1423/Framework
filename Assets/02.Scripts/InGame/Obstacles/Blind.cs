using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blind : MonoBehaviour
{
    public float offset = 7.5f;

    GameManager gameManager;
    private Player player;
    private Transform playerTrasform;

    private void Start()
    {
        gameManager = GameManager.instance;
        player = gameManager.GetPlayer;
        playerTrasform = player.transform;

        Disable();
        //offset = player.transform.position.y - transform.position.y;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }


    private void Update()
    {
        Vector3 playerPos = playerTrasform.position - new Vector3(0, offset);
        transform.position = playerPos;
    }
}
