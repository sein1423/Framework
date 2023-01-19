using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public int expTotal = 50;
    public int expMax = 5000;
    public int combo;

    public int[] starScore_standards; 

    public bool b_startFever = false;
    public bool b_feverDone = false;
    public float f_startCount = 3;
    public int i_startCount = 3;
    public bool b_gameStart = false;
    public bool b_blindActive = false;
    public bool b_isGameOverByFail = false;
    public bool b_isGameOveBySuc = false;

    //Trigger By Skill
    public bool b_magneticItem = false;
    public bool b_doubleScoreItem = false;
    public bool b_obstacleImmuneItem = false;

    public Transform canvasTransform;
    public CameraMove cameraMove;

    private float feverTime = 10;
    private bool b_countDown = true;
    private Player _player;
    public Player GetPlayer
    {
        get { return _player; }
    }

    private Blind blind;
    public Blind GetBlind
    {
        get { return blind; }
    }

    private QuickSlot quickSlot;
    public QuickSlot GetQuickSlot
    {
        get { return quickSlot; }
    }

    UIManager uiManager;
    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
        blind = GameObject.FindObjectOfType<Blind>();
        quickSlot = GameObject.FindObjectOfType<QuickSlot>();
        uiManager = UIManager.instance;
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
        if(b_gameStart)
        {
            if(Player.instance.isDead)
            {
                b_gameStart = false;
            }

            if(combo == 4) // ��ֹ� ����� ����
            {
                Player.instance.SetOriginSize();
                Rigidbody2D rigid = Player.instance.gameObject.GetComponent<Rigidbody2D>();
                rigid.gravityScale = 1;
                GetBlind.Disable();
            }

            if(b_startFever) // Player.cs ���� �ǹ� ���� �����ϸ� �ǹ� ����
            {
                cameraMove.SetFeverFocus();
                feverTime -= Time.deltaTime;
                Destroy(GetPlayer.gameObject.GetComponent<Rigidbody2D>()); // �巡�׷� �����̱� ���ؼ� Rigidbody2D ����, enabled = false ����� ����
                                                                           // ��Ȱ��ȭ�� ���� simulated�� off �ϸ� ����ġ�� �� �Ծ���
                if(feverTime < 0) // �ǹ�Ÿ���� ������ ���� ���·� ����
                {
                    ReturnOriginState();
                }
            }
        }

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (b_isGameOverByFail)
        {
            uiManager.ShowFailMenu(true);
        }

        if (b_isGameOveBySuc)
        {
            int starNum = CountStarForScore();
            uiManager.ShowSucMenu(true, starNum);
        }
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
        GetPlayer.gameObject.AddComponent<Rigidbody2D>(); // ���� �� �߰��� �ǹ�Ÿ�� ���� �� ���� �� �� �ѹ����� �������� �ּ�ȭ
        FeverExpPool.instance.ReturnAll();
        b_feverDone = true;
    }

    private int CountStarForScore()
    {
        int starNum;
        starNum = Random.Range(0, 4); // �ӽ÷� �س���
        return starNum;
    }

    public void AddScore(int exp)
    {
        if (b_doubleScoreItem || b_startFever)
            expTotal += exp * 2;
        else if (b_doubleScoreItem && b_startFever)
            expTotal += exp * 4;
        else
            expTotal += exp;
    }
}


