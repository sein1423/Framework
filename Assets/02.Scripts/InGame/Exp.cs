using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    // 경험치 주는 아이템 스크립트

    public int expGain = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.instance.expTotal += expGain;
            gameObject.SetActive(false);
        }
    }
}
