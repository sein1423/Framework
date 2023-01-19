using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFillAround : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UseSkill);
    }

    private void UseSkill()
    {
        SkillManager.instance.ScoreFillAround();
        Destroy(gameObject);
    }
}
