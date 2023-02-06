using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    private Image expSlider;
    private float exp;
    private float expRatio;
    GameManager gameManager;
    Player player;

    private void Awake()
    {
        gameManager = GameManager.instance;
        player = gameManager.Player;
        expSlider = GetComponent<Image>();
    }
    private void Update()
    {
        exp = gameManager.ExpTotal;
        expRatio = (float)(gameManager.ExpTotal - player.expToDown) / (player.expToUp - player.expToDown);
        switch (player.grade)
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
