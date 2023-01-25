using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text scoreText;
    public Text countDownText;
    public Text feverText;
    public Text feverGuideText;
    public GameObject pauseMenu;
    public GameObject resultMenu;
    public GameObject failMenu;
    public GameObject[] Stars;
    private void Update()
    {
        scoreText.text = "Score : " + GameManager.instance.expTotal;
        ShowCountDown();
        ShowFeverText();
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

    private void ShowFeverText()
    {
        if (GameManager.instance.b_startFever)
        {
            feverText.gameObject.SetActive(true);
            feverGuideText.gameObject.SetActive(true);
        }

        else
        {
            feverText.gameObject.SetActive(false);
            feverGuideText.gameObject.SetActive(false);
        }
    }

    public void SetPauseMenu(bool isActive)
    {
        pauseMenu.SetActive(isActive);
    }

    public void ShowSucMenu(bool isActive, int starNum)
    {
        resultMenu.SetActive(isActive);
        Stars[starNum].SetActive(true);
    }

    public void ShowFailMenu(bool isActive)
    {
        failMenu.SetActive(isActive);
    }

    public void ShowDamagedScore()
    {
        scoreText.color = Color.red;
        StartCoroutine("ChangeOriginColor");
    }

    public void ShowGainScore()
    {
        scoreText.color = Color.blue;
        StartCoroutine("ChangeOriginColor");
    }
    IEnumerator ChangeOriginColor()
    {
        yield return new WaitForSeconds(0.5f);
        scoreText.color = Color.white;
    }
}
