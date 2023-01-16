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
        foreach(Platform platform in platforms) // �ڽ����� �ִ� ��� platform�� ����Ʈ�� �߰�
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
                platformList[i - 1].b_isTarget = false; // ���� target false��
                
                for(int j= 0; j <= i-1; j++) // ������ �÷����� ��� ��Ȱ��ȭ
                    platformList[j].gameObject.SetActive(false);

                platformList[i + 1].b_isTarget = true; // ���� �÷����� target����
            }
        }
    }
}
