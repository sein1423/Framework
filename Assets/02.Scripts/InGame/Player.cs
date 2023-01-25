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
        //�ݶ��̴��� physicsmaterial �Ҵ� �� bounciness ����
        coli = GetComponent<CircleCollider2D>();
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = bounceValue;
        material.friction = frictionValue;
        coli.sharedMaterial = material;
        //

        SetGradeInfo(0); // �ʱ� 0��� ����
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
            if (GameManager.instance.expTotal <= 0) //����ġ = ü�� = 0�� ��
            {
                Dead();
            }

            SetPlayerStatus(); //���� ��޺� ���� ����

            FeverCheck();

            if (isFeverTime) //�ǹ�Ÿ�� ���� ���� �� ���� �ִ� ��ġ �����س��� �ǹ�Ÿ�� ������ �̵�
            {
                playerPrePos = transform.position;
                transform.position = feverTransform.position;
                GameManager.instance.b_startFever = true;
                isFeverTime = false;
            }

            if(GameManager.instance.b_feverDone) // �ǹ��� ������ �� �ǹ� ���� �� ���� ��ġ�� ����
            {
                transform.position = playerPrePos;
                GameManager.instance.b_feverDone = false;
            }
        }
    }
    /// <summary>
    /// ���� ��޺� Status ����
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

        if (!isDmgImmune) // ���� ���°� �ƴ� ��
            expDmg = gradeData.expDmg;
        else //���� ���� �� ��
            expDmg = 0;
    }

    /// <summary>
    /// ���� �޺� �޼� �� �ǹ�Ÿ�� Ȱ��ȭ
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
    /// ��޺� expDmg��ŭ ����
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
