using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text scoreText;
    public Text countDownText;
    public Text comboText;
    private void Update()
    {
        scoreText.text = "Score : " + GameManager.instance.score;
        ShowCountDown();
    }

    /// <summary>
    /// 카운트 다운 표시
    /// </summary>
    private void ShowCountDown()
    {
        countDownText.text = GameManager.instance.i_startCount.ToString();

        if (GameManager.instance.i_startCount == 0)
        {
            countDownText.color = Color.yellow;
            countDownText.text = "Game Start!";
        }
    }
}
