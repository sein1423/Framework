using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public int score;
    public int expTotal = 50;
    public int expMax = 5000;
    public int combo;
    public bool b_startFever = false;
    public bool b_feverDone = false;
    public float f_startCount = 3;
    public int i_startCount = 3;
    public bool b_gameStart = false;

    private float feverTime = 5;
    private float gameTime;
    
    public void AddScore()
    {
        score += 1;
    }

    private void Update()
    {
        //���� ī��Ʈ 3 2 1
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
        }

        //���� �������� ��
        if(b_gameStart)
        {
            gameTime += Time.deltaTime;

            if(Player.instance.isDead)
            {
                b_gameStart = false;
            }

            if(b_startFever) // Player.cs ���� �ǹ� ���� �����ϸ� �ǹ� ����
            {
                feverTime -= Time.deltaTime;
                GameObject playerObject = Player.instance.gameObject;
                Destroy(playerObject.GetComponent<Rigidbody2D>()); // �巡�׷� �����̱� ���ؼ� Rigidbody2D ����
                
                if(feverTime < 0)
                {
                    b_startFever = false;
                    feverTime = 5;
                    playerObject.AddComponent<Rigidbody2D>(); // �ٽ� �߰�
                    FeverExpPool.instance.ReturnAll();
                    b_feverDone = true;
                }
            }
        }
    }
}
