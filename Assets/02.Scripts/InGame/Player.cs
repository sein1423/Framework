using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public float bounceValue;
    public float frictionValue;
    public bool isDead = false;
    public bool isFeverTime = false;
    public bool isDmgImmune = false;
    public int grade = 0;

    public Sprite[] spriteAsset;
    public Transform feverTransform;

    [SerializeField]
    private StatusDatas statusDatas;

    private int gradeIdx = 0;
    public int expToUp;
    public int expToDown;
    public int expDmg;

    private Vector3 playerPrePos;
    private Vector3 playerOriginSize;
    private SpriteRenderer spriteRenderer;
    private AudioSource expGainSound;
    private Rigidbody2D rigid;
    private const int GRADE_MAX = 4;

    private CircleCollider2D coli;

    private void Awake()
    {
        //콜라이더에 physicsmaterial 할당 후 bounciness 설정
        coli = GetComponent<CircleCollider2D>();
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = bounceValue;
        material.friction = frictionValue;
        coli.sharedMaterial = material;
        //

        SetGradeInfo(0); // 초기 0등급 설정
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        expGainSound = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        playerOriginSize = gameObject.transform.localScale;

        Input.gyro.enabled = true;
    }
    private void Update()
    {
        if (GameManager.instance.b_gameStart)
        {
            if (GameManager.instance.expTotal <= 0) //경험치 = 체력 = 0일 때
            {
                Dead();
            }

            SetPlayerStatus(); //공의 등급별 설정 적용

            FeverCheck();

            if (isFeverTime) //피버타임 조건 만족 시 원래 있던 위치 저장해놓고 피버타임 맵으로 이동
            {
                playerPrePos = transform.position;
                transform.position = feverTransform.position;
                GameManager.instance.b_startFever = true;
                isFeverTime = false;
            }

            if(GameManager.instance.b_feverDone) // 피버가 끝났을 때 피버 시작 전 기존 위치로 복귀
            {
                transform.position = playerPrePos;
                GameManager.instance.b_feverDone = false;
            }
        }
    }
    /// <summary>
    /// 공의 등급별 Status 적용
    /// </summary>
    private void SetPlayerStatus()
    {
        if(GameManager.instance.expTotal >= expToUp)
        {
            gradeIdx++;
            if (gradeIdx > GRADE_MAX)
                gradeIdx = GRADE_MAX;
            SetGradeInfo(gradeIdx);
        }

        if(GameManager.instance.expTotal < expToDown)
        {
            gradeIdx--;
            SetGradeInfo(gradeIdx);
        }

        spriteRenderer.sprite = spriteAsset[grade];
    }

    private void SetGradeInfo(int idx)
    {
        StatusData gradeData = statusDatas.GetData(idx);
        grade = gradeData.grade;
        expToUp = gradeData.expToUp;
        expToDown = gradeData.expToDown;

        if (!isDmgImmune) // 무적 상태가 아닐 때
            expDmg = gradeData.expDmg;
        else //무적 상태 일 때
            expDmg = 0;
    }

    /// <summary>
    /// 일정 콤보 달성 시 피버타임 활성화
    /// </summary>
    private void FeverCheck()
    {
        if (GameManager.instance.combo == 5)
        {
            SetSize(playerOriginSize);
            isFeverTime = true;
            GameManager.instance.combo = 0;
        }
    }
    /// <summary>
    /// 등급별 expDmg만큼 피해
    /// </summary>
    public void Damage()
    {
        GameManager.instance.expTotal -= expDmg;
        UIManager.instance.ShowDamagedScore();
    }

    public void SetSize(Vector3 size)
    {
        gameObject.transform.localScale = size;
    }
    public void Dead()
    {
        isDead = true;
        GameManager.instance.b_isGameOverByFail = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Exp"))
        {
            expGainSound.Play();
        }
    }

    public void SetOriginSize()
    {
        SetSize(playerOriginSize);
    }
}
