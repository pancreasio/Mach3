using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;


public class AdManager : MonoBehaviour
{
    private const string androidID = "3317305";

    private void Awake()
    {
        Advertisement.Initialize(androidID, true);
    }

    public void WatchVideoAd(ShowResult result)
    {
        Advertisement.Show("video");
        switch (result)
        {
            case ShowResult.Failed:
                break;
            default:
                break;
        }
    }

    public void WatchRewardedVideoAd()
    {
        Advertisement.Show("rewardedVideo");
    }
}
