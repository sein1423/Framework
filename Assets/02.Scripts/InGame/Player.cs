using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public float bounceValue;
    public float frictionValue;
    private bool isDead = false;
    public bool IsDead { get => isDead; }
    public bool isFeverTime = false;
    public bool isDmgImmune = false;
    public int grade = 0;

    public Sprite[] spriteAsset;
    public Transform feverTransform;

    [SerializeField]
    private StatusDatas statusDatas;

    private int gradeIdx = 0;
    private float originY;
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

    GameManager gameManager;
    UIManager uIManager;

    private void Awake()
    {
        //콜라이더에 physicsmaterial 할당 후 bounciness 설정
        coli = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        expGainSound = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();

        CreatePhysicsMaterial();
        SetGradeInfo(0); // 초기 0등급 설정

        uIManager = UIManager.instance;
        gameManager = GameManager.instance;

        playerOriginSize = gameObject.transform.localScale;
    }

    void CreatePhysicsMaterial()
    {
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = bounceValue;
        material.friction = frictionValue;
        coli.sharedMaterial = material;
    }

    private void Update()
    {
        if (gameManager.b_GameStart)
        {
            SetPlayerStatus(); //공의 등급별 설정 적용
        }
    }
    /// <summary>
    /// 공의 등급별 Status 적용
    /// </summary>
    private void SetPlayerStatus()
    {
        if(gameManager.ExpTotal >= expToUp)
        {
            gradeIdx++;
            if (gradeIdx > GRADE_MAX)
                gradeIdx = GRADE_MAX;
            SetGradeInfo(gradeIdx);
        }

        if(gameManager.ExpTotal < expToDown)
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

   

    public void StartFever()
    {
        SetSize(playerOriginSize);

        playerPrePos = transform.position;
        transform.position = feverTransform.position;

        DisableCollision(true);
    }

    public void EndFever()
    {
        transform.position = playerPrePos;
        DisableCollision(false);
    }

    void DisableCollision(bool active)
    {
        rigid.isKinematic = active;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
    }

    /// <summary>
    /// 등급별 expDmg만큼 피해
    /// </summary>
    public void Damage()
    {
        gameManager.AddScore(-expDmg);
        UIManager.instance.ShowDamagedScore();
    }

    public void SetSize(Vector3 size)
    {
        gameObject.transform.localScale = size;
    }
    public void Dead()
    {
        isDead = true;
        gameManager.b_isGameOverByFail = true;
        originY = gameObject.transform.position.y;
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        gameManager.RevivePlayer();

        isDead = false;
        originY += 10;
        transform.position = new Vector3(0, originY + 5f);
        gameObject.SetActive(true);
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

    public void DisableDebuf()
    {
        SetOriginSize();
        rigid.gravityScale = 1;
    }
}
