using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;

public class GoogleAdsManager : MonoBehaviour
{
    string adUnitId;

    private RewardedAd rewardedAd;

    GameManager gameManager;
    UIManager uiManager;
    public void Start() //���� �ʱ�ȭ
    {
        //test ���� id : ca-app-pub-3940256099942544/5224354917
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-2630325794227688/3625700458"; //Test ���� Id
#elif UNITY_IOS
		adUnitId = "ca-app-pub-6754544778509872/7165886378"; //Test ���� Id
#else
		adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);

        gameManager = GameManager.instance;
        uiManager = UIManager.instance;
    }

    public void Show() //����
    {
        
        StartCoroutine(ShowRewardAd());
    }

    IEnumerator ShowRewardAd() //���� �����ֱ�
    {
        while (!rewardedAd.IsLoaded())
        {
            yield return null;
        }
        rewardedAd.Show();
    }

    public void ReloadAd() //���� �ٽ� �ε��ϱ�
    {
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-6754544778509872/8238444835";

#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }


    public void HandleRewardedAdClosed(object sender, EventArgs args) //���� ������ ����
    {
        ReloadAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args) //���� ������ ������ �޴� ����
    {
        gameManager.Player.Revive();
    }
}