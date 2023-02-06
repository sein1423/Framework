using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverExpSpawner : MonoBehaviour
{
    private float spawnTime;
    private float randomX;
    public Transform feverBoard;
    FeverExpPool feverExpPool;
    GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.instance;
        feverExpPool = FeverExpPool.instance;
    }


    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (gameManager.b_StartFever)
        {
            if (spawnTime > 0.2f) // spawnTime마다 생성해서 랜덤 위치
            {
                GameObject expObject = feverExpPool.RandomGetExpObject(); //풀의 경험치 프리팹들 중 랜덤으로 가져온다
                randomX = Random.Range(-3f, 3f);
                expObject.transform.position = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
                spawnTime = 0;
            }
        }
    }


}
