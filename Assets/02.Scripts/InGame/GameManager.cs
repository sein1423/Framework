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
        //���� ī��Ʈ 3 2 1
        {
            if (f_startCount > 0)
            {
                f_startCount -= Time.deltaTime;
                i_startCount = Mathf.RoundToInt(f_startCount);
            }
            //ī��Ʈ �� ���� ����
            else
            {
                UIManager.instance.countDownText.gameObject.SetActive(false);
                b_gameStart = true;
                b_countDown = false;
            }
        }

        //���� �������� ��
        if (b_gameStart)
        {
            if (ExpTotal <= 0) //����ġ = ü�� = 0�� ��
            {
                player.Dead();
            }

            if (Player.IsDead)
            {
                b_gameStart = false;
            }

            if (combo == 4) // ��ֹ� ����� ����
            {
                Player.DisableDebuf();
                blind.Disable();
            }

            if(FeverCheck())
            {
                StartFever();
            }

            if (b_startFever) // Player.cs ���� �ǹ� ���� �����ϸ� �ǹ� ����
            {
                cameraMove.SetFeverFocus();
                feverTime -= Time.deltaTime;
                //Destroy(GetPlayer.gameObject.GetComponent<Rigidbody2D>()); // �巡�׷� �����̱� ���ؼ� Rigidbody2D ����, enabled = false ����� ����
                // ��Ȱ��ȭ�� ���� simulated�� off �ϸ� ����ġ�� �� �Ծ���
                if (feverTime < 0) // �ǹ�Ÿ���� ������ ���� ���·� ����
                {
                    EndFever();
                }
            }
        }

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
        //GetPlayer.gameObject.AddComponent<Rigidbody2D>(); // ���� �� �߰��� �ǹ�Ÿ�� ���� �� ���� �� �� �ѹ����� �������� �ּ�ȭ
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
            b_gameDone = true;
            if (b_gameDone)
            {
                CountStarForScore();
                SaveStageInfo(starNum);
                ResultGainCoin();
                b_gameDone = false;
            }
            uiManager.ShowSucMenu(true, starNum);
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

    private void SaveStageInfo(int star)
    {
        if (Global.Instance.Star < starNum)
        {
            Global.Instance.Star = star;
        }

        if (Global.Instance.HIghScore < expTotal)
        {
            Global.Instance.HIghScore = expTotal;
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
    /// ���� �޺� �޼� �� �ǹ�Ÿ�� Ȱ��ȭ
    /// </summary>
    private bool FeverCheck()
    {
        if (combo == 5)
            return true;
        return false;
    }
}


