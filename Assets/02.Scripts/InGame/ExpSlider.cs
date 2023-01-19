using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    private Image expSlider;
    private float exp;
    private float expRatio;
    private void Awake()
    {
        expSlider = GetComponent<Image>();
    }
    private void Update()
    {
        exp = GameManager.instance.expTotal;
        expRatio = (float)(GameManager.instance.expTotal- Player.instance.expToDown) / (Player.instance.expToUp - Player.instance.expToDown);
        switch (Player.instance.grade)
        {
            case 0:
                expSlider.fillAmount = expRatio * 0.25f;
                break;
            case 1:
                expSlider.fillAmount = 0.25f + expRatio * 0.25f;
                break;
            case 2:
                expSlider.fillAmount = 0.5f + expRatio * 0.25f; ;
                break;
            case 3:
                expSlider.fillAmount = 0.75f + expRatio * 0.25f; ;
                break;
            case 4:
                expSlider.fillAmount = 1;
                break;
        }
    }
}
