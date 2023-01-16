using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverExpSpawner : MonoBehaviour
{
    private float spawnTime;
    private float randomX;
    public Transform feverBoard;
    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (GameManager.instance.b_startFever)
        {
            if (spawnTime > 0.2f) // spawnTime마다 생성해서 랜덤 위치
            {
                GameObject expObject = FeverExpPool.instance.GetExpObject(Random.Range(0, FeverExpPool.instance.pools.Length)); //풀의 경험치 프리팹들 중 랜덤으로 가져온다
                randomX = Random.Range(-3f, 3f);
                expObject.transform.position = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
                spawnTime = 0;
            }
        }
    }


}
