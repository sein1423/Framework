using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    // ����ġ �ִ� ������ ��ũ��Ʈ

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
