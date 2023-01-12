using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusData
{
    public int grade;
    public int expToUp;
    public int expDmg;
}

[CreateAssetMenu(fileName = "GradeData", menuName = "Scriptable Object/Grade Data", order = 51)]
public class StateDatas : ScriptableObject
{
    [SerializeField]
    public List<StatusData> StatusDatas;

    public StatusData GetData(int idx)
    {
        return StatusDatas[idx];
    }
}
