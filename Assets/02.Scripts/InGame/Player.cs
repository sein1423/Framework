using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public float bounceValue;
    public float frictionValue;
    public bool isDead = false;
    public bool isFeverTime = false;
    public int grade = 0;
    public Sprite[] spriteAsset;
    public GameObject comboTextPrefab;
    public Transform feverTransform;

    [SerializeField]
    private StateDatas stateDatas;

    private int expToUp;
    private int expToDown;
    private int expDmg;
    private Vector3 playerPrePos;
    private SpriteRenderer spriteRenderer;
    private const int GRADE_MAX = 4;

    private CircleCollider2D coli;

    private void OnEnable()
    {
        //콜라이더에 physicsmaterial 할당 후 bounciness 설정
        coli = GetComponent<CircleCollider2D>();
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = bounceValue;
        material.friction = frictionValue;
        coli.sharedMaterial = material;
        //

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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
        switch (grade)
        {
            case 0:
                expToUp = 100;
                expDmg = 20;
                break;

            case 1:
                expToDown = 100;
                expToUp = 300;
                expDmg = 30;
                break;
            case 2:
                expToDown = 300;
                expToUp = 1000;
                expDmg = 50;
                break;

            case 3:
                expToDown = 1000;
                expToUp = 5000;
                expDmg = 100;
                break;

            case 4:
                expToDown = 5000;
                expDmg = 150;
                break;
        }

        if (GameManager.instance.expTotal >= expToUp && grade != GRADE_MAX)
        {
            grade++;
        }

        if (GameManager.instance.expTotal < expToDown && grade != 0)
        {
            grade--;
        }

        spriteRenderer.sprite = spriteAsset[grade];
    }
    /// <summary>
    /// 일정 콤보 달성 시 피버타임 활성화
    /// </summary>
    private void FeverCheck()
    {
        if (GameManager.instance.combo == 5)
        {
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
    }

    public void Dead()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
