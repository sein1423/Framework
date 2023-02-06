using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverExpPool : Singleton<FeverExpPool>
{
    public GameObject[] expPrefabs;
    public int initCount = 20;

    public List<GameObject>[] pools;

    private void Awake()
    {
        Initialize();
    }
    /// <summary>
    /// 오브젝트 풀링, 미리 생성하기
    /// </summary>
    private void Initialize()
    {
        pools = new List<GameObject>[expPrefabs.Length];

        //initCount만큼 미리 생성해두기
        for (int idx = 0; idx < expPrefabs.Length; idx++)
        {
            pools[idx] = new List<GameObject>();

            for (int i = 0; i < initCount; i++)
            {
                GameObject expObject = Instantiate(expPrefabs[idx], transform);
                expPrefabs[idx].SetActive(false);
                pools[idx].Add(expObject);
            }
        }
    }
   
    /// <summary>
    /// pool에서 exp 아이템 가져오기
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public GameObject GetExpObject(int idx)
    {
        GameObject expObject = null;
        foreach (GameObject item in pools[idx])
        {
            if (!item.activeSelf)
            {
                expObject = item;
                expObject.SetActive(true);
                break;
            }
        }

        if(!expObject)
        {
            expObject = Instantiate(expPrefabs[idx], transform);
            pools[idx].Add(expObject);
        }

        return expObject;
    }

    public GameObject RandomGetExpObject(int start = 0, int end = 0)
    {
        int rndIdx = 0;
        if (start == 0 && end == 0)
            rndIdx = Random.Range(0, pools.Length);
        else
            rndIdx = Random.Range(start, end);

        return GetExpObject(rndIdx);
    }

    /// <summary>
    /// exp 아이템 모두 비활성화
    /// </summary>
    public void ReturnAll()
    {
        for (int idx = 0; idx < expPrefabs.Length; idx++)
        {
            for (int i = 0; i < pools[idx].Count; i++)
            {
                pools[idx][i].SetActive(false);
            }
        }
    }
}
