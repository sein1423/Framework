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
    public Text gainCoinAmountText;
    public GameObject pauseMenu;
    public GameObject resultMenu;
    public GameObject failMenu;
    public GameObject failReviveMenu;
    public GameObject feverGuidePanel;

    public GameObject blindGuidePanel;
    public GameObject bounceOffGuidePanel;
    public GameObject scoreDownGuidePanel;
    public GameObject sizeDownGuidePanel;
    public GameObject speedUpGuidePanel;

    public GameObject obstacleImmuneGuidePanel;
    public GameObject scoreDoubleGuidePanel;
    public GameObject scoreFillAroundGuidePanel;
    public GameObject scoreMagneticGuidePanel;
    public GameObject scoreRandomGainGuidePanel;

    public GameObject[] Stars;

    private int i_reviveCount = 5;
    public float f_reviveTimer = 5;
    private float disablePopUpTime = 1.0f;

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
        gainCoinAmountText.text = GameManager.instance.gainCoinAmount.ToString();
    }

    public void ShowFailReviveMenu(bool isActive)
    {
        failReviveMenu.SetActive(isActive);
        f_reviveTimer -= Time.deltaTime;
        i_reviveCount = Mathf.RoundToInt(f_reviveTimer);
        reviveCountText.text = i_reviveCount.ToString();
        if (f_reviveTimer < 0)
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
            yield return new WaitForSeconds(0.5f);
            Destroy(feverGuidePanel);
        }
    }

    #region Obstacle And Skill
    public void ShowBlindGuide()
    {
        blindGuidePanel.SetActive(true);
        StartCoroutine("DisableBlind");
    }

    IEnumerator DisableBlind()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        blindGuidePanel.SetActive(false);
    }

    public void ShowBounceOffGuide()
    {
        bounceOffGuidePanel.SetActive(true);
        StartCoroutine("DisableBounceOff");
    }

    IEnumerator DisableBounceOff()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        bounceOffGuidePanel.SetActive(false);
    }

    public void ShowScoreDownGuide()
    {
        scoreDownGuidePanel.SetActive(true);
        StartCoroutine("DisableScoreDown");
    }

    IEnumerator DisableScoreDown()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        scoreDownGuidePanel.SetActive(false);
    }

    public void ShowSizeDownGuide()
    {
        sizeDownGuidePanel.SetActive(true);
        StartCoroutine("DisableSizeDown");
    }

    IEnumerator DisableSizeDown()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        sizeDownGuidePanel.SetActive(false);
    }

    public void ShowSpeedUpGuide()
    {
        speedUpGuidePanel.SetActive(true);
        StartCoroutine("DisableSpeedUp");
    }

    IEnumerator DisableSpeedUp()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        speedUpGuidePanel.SetActive(false);
    }

    public void ShowObstacleImmuneGuide()
    {
        obstacleImmuneGuidePanel.SetActive(true);
        StartCoroutine("DisableObstacleImmune");
    }

    IEnumerator DisableObstacleImmune()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        obstacleImmuneGuidePanel.SetActive(false);
    }

    public void ShowScoreDoubleGuide()
    {
        scoreDoubleGuidePanel.SetActive(true);
        StartCoroutine("DisableScoreDouble");
    }

    IEnumerator DisableScoreDouble()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        scoreDoubleGuidePanel.SetActive(false);
    }

    public void ShowScoreFillAroundGuide()
    {
        scoreFillAroundGuidePanel.SetActive(true);
        StartCoroutine("DisableScoreFillAround");
    }    

    IEnumerator DisableScoreFillAround()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        scoreFillAroundGuidePanel.SetActive(false);
    }

    public void ShowScoreMagneticGuide()
    {
        scoreMagneticGuidePanel.SetActive(true);
        StartCoroutine("DisableScoreMagnetic");
    }

    IEnumerator DisableScoreMagnetic()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        scoreMagneticGuidePanel.SetActive(false);
    }

    public void ShowScoreRandomGainGuide()
    {
        scoreRandomGainGuidePanel.SetActive(true);
        StartCoroutine("DisableScoreRandomGain");
    }

    IEnumerator DisableScoreRandomGain()
    {
        yield return new WaitForSeconds(disablePopUpTime);
        scoreRandomGainGuidePanel.SetActive(false);
    }

    #endregion
}

