using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Platform[] platforms;
    public List<Platform> platformList;
    public int targetIdx = 1;

    private void Awake()
    {
        platforms = GetComponentsInChildren<Platform>();
        foreach(Platform platform in platforms) // 자식으로 있는 모든 platform을 리스트에 추가
        {
            platformList.Add(platform);
        }
        platformList[0].b_isSteped = true;
    }

    private void Update()
    {
        for(int i = 1; i<platformList.Count-1; i++)
        {
            if (platformList[i].b_isSteped == true)
            {
                platformList[i - 1].b_isTarget = false; // 이전 target false로
                
                for(int j= 0; j <= i-1; j++) // 지나간 플랫폼들 모두 비활성화
                    platformList[j].gameObject.SetActive(false);

                platformList[i + 1].b_isTarget = true; // 다음 플랫폼을 target으로
            }
        }
    }
}
