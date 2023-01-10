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
        //�ݶ��̴��� physicsmaterial �Ҵ� �� bounciness ����
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
    /// ���� �޺� �޼� �� �ǹ�Ÿ�� Ȱ��ȭ
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
    /// ��޺� expDmg��ŭ ����
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
