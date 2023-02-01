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
    public Text reviveCountText;
    public GameObject pauseMenu;
    public GameObject resultMenu;
    public GameObject failMenu;
    public GameObject failReviveMenu;
    public GameObject feverGuidePanel;
    public GameObject[] Stars;

    private int i_reviveCount = 5;
    public float f_reviveTimer = 5;

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
            StartCoroutine("ShowFeverGuide");
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

    public void ShowFailReviveMenu(bool isActive)
    {
        failReviveMenu.SetActive(isActive);
        f_reviveTimer -= Time.deltaTime;
        i_reviveCount = Mathf.RoundToInt(f_reviveTimer);
        reviveCountText.text = i_reviveCount.ToString();
        if(f_reviveTimer < 0)
        {
            ShowFailMenu();
        }
    }

    public void ShowFailMenu()
    {
        failReviveMenu.SetActive(false);
        failMenu.SetActive(true);
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

    IEnumerator ShowFeverGuide()
    {
        if (feverGuidePanel != null)
        {
            feverGuidePanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            Destroy(feverGuidePanel);
        }
    }
}
