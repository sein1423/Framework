using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Experimental.GraphView.GraphView;
//using static UnityEngine.UI.Image;

public class GameManager : Singleton<GameManager>
{
    private int expTotal = 50;
    public int ExpTotal
    {
        get => expTotal;
    }

    public int expMax = 5000;
    public int combo;
    public int gainCoinAmount;

    public int[] starScore_standards;
    private int starNum;

    private bool b_startFever = false;
    private bool b_CoinGainOnce = false;
    public bool b_StartFever { get => b_startFever; }
    public bool b_feverDone = false;
    public float f_startCount = 3;
    public int i_startCount = 3;

    private bool b_gameStart = false;
    public bool b_GameStart
    {
        get { return b_gameStart; }
    }

    public bool b_blindActive = false;
    public bool b_isGameOverByFail = false;
    public bool b_isGameOverBySuc = false;
    public bool b_revive = false;
    public bool b_gameDone = false;

    //Trigger By Skill
    public bool b_magneticItem = false;
    public bool b_doubleScoreItem = false;
    public bool b_obstacleImmuneItem = false;

    public Transform canvasTransform;
    public CameraMove cameraMove;
    private GameBoard gameBoard;
    public GameBoard GetGameBoard
    {
        get { return gameBoard; }
    }

    private float feverTime = 10;
    private bool b_countDown = true;
    private Player player;
    public Player Player
    {
        get { 
            if (player == null)
                player = GameObject.FindObjectOfType<Player>();
            return player; 
        }
    }

    public Blind blind;

    private QuickSlot quickSlot;
    public QuickSlot GetQuickSlot
    {
        get { return quickSlot; }
    }

    UIManager uiManager;

    private void Awake()
    {
        blind = GameObject.FindObjectOfType<Blind>();
        quickSlot = GameObject.FindObjectOfType<QuickSlot>();
        gameBoard = GameObject.FindObjectOfType<GameBoard>();
        uiManager = UIManager.instance;
    }

    private void Start()
    {
        SceneManager.LoadSceneAsync(Global.Instance.StageNumber, LoadSceneMode.Additive);
    }

    private void Update()
    {
        cameraMove.SetPlayerFocus();

        if (b_countDown)
        //시작 카운트 3 2 1
        {
            if (f_startCount > 0)
            {
                f_startCount -= Time.deltaTime;
                i_startCount = Mathf.RoundToInt(f_startCount);
            }
            //카운트 후 게임 시작
            else
            {
                UIManager.instance.countDownText.gameObject.SetActive(false);
                b_gameStart = true;
                b_countDown = false;
            }
        }

        //게임 시작했을 때
        if (b_gameStart)
        {
            if (ExpTotal <= 0) //경험치 = 체력 = 0일 때
            {
                player.Dead();
            }

            if (Player.IsDead)
            {
                b_gameStart = false;
            }

            if (combo == 4) // 장애물 디버프 해제
            {
                Player.DisableDebuf();
                blind.Disable();
            }

            if(FeverCheck())
            {
                StartFever();
            }

            if (b_startFever) // Player.cs 에서 피버 조건 만족하면 피버 시작
            {
                cameraMove.SetFeverFocus();
                feverTime -= Time.deltaTime;
                //Destroy(GetPlayer.gameObject.GetComponent<Rigidbody2D>()); // 드래그로 움직이기 위해서 Rigidbody2D 삭제, enabled = false 기능이 없음
                // 비활성화를 위해 simulated를 off 하면 경험치가 안 먹어짐
                if (feverTime < 0) // 피버타임이 끝나면 원래 상태로 복귀
                {
                    EndFever();
                }
            }
        }

        if(!b_gameDone)
            CheckGameOver();
    }

    public void BlindItem_TriggerByPlayer()
    {
        b_blindActive = true;
        combo = 0;
        blind.Enable();
    }

    private void ReturnOriginState()
    {
        b_startFever = false;
        feverTime = 10;
        //GetPlayer.gameObject.AddComponent<Rigidbody2D>(); // 삭제 및 추가를 피버타임 진행 및 종료 시 딱 한번씩만 진행으로 최소화
        FeverExpPool.instance.ReturnAll();
        b_feverDone = true;
    }

    private void CheckGameOver()
    {
        if (b_isGameOverByFail)
        {
            uiManager.ShowFailReviveMenu(true);
        }

        if (b_isGameOverBySuc)
        {
 
            CountStarForScore();
            SaveStageInfo();
            ResultGainCoin();
            Debug.Log("GAME DONE");
            uiManager.ShowSucMenu(true, starNum);
            b_gameDone = true;
        }
    }

    private void CountStarForScore()
    {
        if (Global.Instance.Stage <= 6)
        {
            starNum = 3;
        }

        else
        { 
            if (expTotal < 3000)
            {
                starNum = 1;
            }

            else if (expTotal < 5000)
            {
                starNum = 2;
            }

            else
            {
                starNum = 3;
            }
        }
    }

    private void SaveStageInfo()
    {
        if (!Global.Instance.StageDict.ContainsKey(Global.Instance.StageNumber))
            Global.Instance.StageDict.Add(Global.Instance.StageNumber, starNum);

        else
        {
            if (Global.Instance.StageDict[Global.Instance.StageNumber] < starNum)
            {
                Global.Instance.StageDict[Global.Instance.StageNumber] = starNum;
            }
        }

        Global.Instance.SaveData();
    }

    private void ResultGainCoin()
    {

        gainCoinAmount = starNum * 10 + Global.Instance.Stage * 10;
        Global.Instance.Coin += gainCoinAmount;
        
    }
    public void AddScore(int exp)
    {
        if (exp > 0)
        {
            if (b_doubleScoreItem || b_startFever)
                expTotal += exp * 2;
            else if (b_doubleScoreItem && b_startFever)
                expTotal += exp * 4;
            else
                expTotal += exp;
        }
        else
            expTotal += exp;

    }

    public void RevivePlayer()
    {
        if (expTotal <= 0) // Dead by Damage
        {
            expTotal = 50;
        }

        b_revive = true;
        b_gameStart = true;
        b_isGameOverByFail = false;

        uiManager.failReviveMenu.SetActive(false);
        uiManager.f_reviveTimer = 5;
    }


    public void OnCollisionByObstacle(BounceOff obstacle, int decreaseExp)
    {
        uiManager.ShowBounceOffGuide();

        if (!b_obstacleImmuneItem)
        {
            AddScore(decreaseExp);
            combo = 0;
            uiManager.ShowDamagedScore();
        }
        else
        {
            b_obstacleImmuneItem = false;
            obstacle.Disable();
        }
    }

    public void Success()
    {
        b_isGameOverBySuc = true;
        b_gameStart = false;
        player.gameObject.SetActive(false);
    }

    public void StartFever()
    {
        combo = 0;
        b_startFever = true;
        Player.StartFever();
        uiManager.ShowFeverText(true);
    }

    void EndFever()
    {
        b_startFever = false;
        Player.EndFever();
        uiManager.ShowFeverText(false);
        ReturnOriginState();
    }

    /// <summary>
    /// 일정 콤보 달성 시 피버타임 활성화
    /// </summary>
    private bool FeverCheck()
    {
        if (combo == 5)
            return true;
        return false;
    }
}


