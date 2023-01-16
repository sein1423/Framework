using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlatform : MonoBehaviour
{
    private List<float> skillPercentList;
    private float randomPercent;
    private bool isSteped;
    private int skillNum;
    public float scoreDoublePercent = 0.36f;
    public float scoreMagneticPercent = 0.35f;
    public float scoreFillAroundPercent = 0.07f;
    public float obstacleImmunePercent = 0.07f;
    public float scoreRandomGainPercent = 0.15f;

    public GameObject[] itemPrefabs;

    public float[] scorePercent = { 0.36f, 0.71f, 0.78f, 0.85f, 1.0f };

    private void Awake()
    {
        skillPercentList = new List<float>();
        skillPercentList.Add(scoreDoublePercent);
        skillPercentList.Add(scoreMagneticPercent);
        skillPercentList.Add(scoreFillAroundPercent);
        skillPercentList.Add(obstacleImmunePercent);
        skillPercentList.Add(scoreRandomGainPercent);

        isSteped = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isSteped)
        {
            QuickSlot quickSlot = collision.gameObject.GetComponentInChildren<QuickSlot>();

            RandomSkillSetting();
            
            if (skillNum == 4)
            {
                SkillManager.instance.ScoreGainByPercent();
            }

            else
            {
                for (int i = 0; i < quickSlot.slots.Count; i++)
                {
                    if (quickSlot.slots[i].isEmpty)
                    {
                        Instantiate(itemPrefabs[skillNum], quickSlot.slots[i].slotObj.transform, false);
                        quickSlot.slots[i].isEmpty = false;
                        break;
                    }
                }
            }
            isSteped = true;
            
        }
    }

    private void RandomSkillSetting()
    {
        randomPercent = Random.Range(0, 1f);

        for (int i = 0; i < scorePercent.Length; i++)
        {
            if (randomPercent <= scorePercent[i])
            {
                skillNum = i;
                break;
            }
        }
    }
}
