using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public int[] scores = { 100, 200, 300, 400, 500, 5000 };
    public float[] scorePercent = { 0.69f, 0.91f, 0.96f, 0.98f, 0.99f, 1.0f };

    public int fillAmount = 20;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    public void ScoreGainByPercent()
    {
        int score = 0;
        float percent;
        percent = Random.Range(0, 1f);
        for (int i = 0; i < scorePercent.Length; i++)
        {
            if (percent <= scorePercent[i])
            {
                score = scores[i];
                break;
            }
        }
        UIManager.instance.ShowGainScore();
        gameManager.AddScore(score);
    }

    public void ScoreDouble()
    {
        gameManager.b_doubleScoreItem = true;
        StartCoroutine("ScoreDoubleDelay");
    }

    IEnumerator ScoreDoubleDelay()
    {
        yield return new WaitForSeconds(10f);
        gameManager.b_doubleScoreItem = false;
    }

    public void ScoreFillAround()
    {
        for (int i = 0; i < fillAmount; i++)
        {
            Transform playerTransform = gameManager.Player.transform;
            GameObject expObject = FeverExpPool.instance.GetExpObject(Random.Range(0, FeverExpPool.instance.pools.Length));
            float randomX = Random.Range(-3f, 3f);
            float randomY = Random.Range(0, 7f);
            expObject.transform.position = new Vector3(randomX, playerTransform.position.y - randomY);

        }
    }

    public void ScoreMagnetic()
    {
        gameManager.b_magneticItem = true;
        StartCoroutine("ScoreMagneticDelay");
    }

    IEnumerator ScoreMagneticDelay()
    {
        yield return new WaitForSeconds(5f);
        gameManager.b_magneticItem = false;
    }

    public void ObstacleImmune()
    {
        gameManager.b_obstacleImmuneItem = true;
    }
}
