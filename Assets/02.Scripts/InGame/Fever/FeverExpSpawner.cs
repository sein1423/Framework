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
            if (spawnTime > 0.2f) // spawnTime���� �����ؼ� ���� ��ġ
            {
                GameObject expObject = FeverExpPool.instance.GetExpObject(Random.Range(0, FeverExpPool.instance.pools.Length)); //Ǯ�� ����ġ �����յ� �� �������� �����´�
                randomX = Random.Range(-3f, 3f);
                expObject.transform.position = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
                spawnTime = 0;
            }
        }
    }


}
