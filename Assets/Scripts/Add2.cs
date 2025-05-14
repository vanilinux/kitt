using System;
using UnityEngine;
using YandexMobileAds.Base;
using YandexMobileAds;
using UnityEngine.UI;
using System.Drawing;
using TMPro;

public class Add2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coins;
    private RewardedAdLoader rewardedAdLoader;
    private RewardedAd rewardedAd;

    public void Awake()
    {
        this.rewardedAdLoader = new RewardedAdLoader();
        this.rewardedAdLoader.OnAdLoaded += this.HandleAdLoaded;
        this.rewardedAdLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        RequestRewardedAd();
        gameObject.GetComponent<Button>().onClick.AddListener(ShowRewardedAd);
        DontDestroyOnLoad(gameObject);
    }



    private void RequestRewardedAd()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        if (this.rewardedAd != null)
        {
            this.rewardedAd.Destroy();
        }

        // Replace demo Unit ID 'demo-rewarded-yandex' with actual Ad Unit ID
        string adUnitId = "demo-rewarded-yandex";

        this.rewardedAdLoader.LoadAd(this.CreateAdRequest(adUnitId));
    }

    private void ShowRewardedAd()
    {
        if (this.rewardedAd == null)
        {
            return;
        }

        this.rewardedAd.OnAdClicked += this.HandleAdClicked;
        this.rewardedAd.OnAdShown += this.HandleAdShown;
        this.rewardedAd.OnAdFailedToShow += this.HandleAdFailedToShow;
        this.rewardedAd.OnAdImpression += this.HandleImpression;
        this.rewardedAd.OnAdDismissed += this.HandleAdDismissed;
        this.rewardedAd.OnRewarded += this.HandleRewarded;

        this.rewardedAd.Show();
    }

    private AdRequestConfiguration CreateAdRequest(string adUnitId)
    {
        return new AdRequestConfiguration.Builder(adUnitId).Build();
    }

    public void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
    {
        this.rewardedAd = args.RewardedAd;
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
    }

    public void HandleAdShown(object sender, EventArgs args)
    {
    }

    public void HandleAdDismissed(object sender, EventArgs args)
    {
        this.rewardedAd.Destroy();
        this.rewardedAd = null;
        RequestRewardedAd();
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
    }

    public void HandleRewarded(object sender, Reward args)
    {
        int b = PlayerPrefs.GetInt("Coins");
        int c = b + 500;
        PlayerPrefs.SetInt("Coins", c);
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
        PlayerPrefs.Save();
    }

    public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        RequestRewardedAd();
    }

}