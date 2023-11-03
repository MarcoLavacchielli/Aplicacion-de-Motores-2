using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string gameID = "5466929", rewardedAdID = "Rewarded_Android";

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID);
    }

    public void ShowAd()
    {
        if (!Advertisement.IsReady()) return;

        Advertisement.Show(rewardedAdID);
    }

    public void OnUnityAdsDidReady(string message)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Ad ready");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Showing Ad");
    }


    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Ad failed");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showresult)
    {
        if (placementId == rewardedAdID)
        {

            if (showresult == ShowResult.Finished) Debug.Log("Full rewards");
            else if (showresult == ShowResult.Skipped) Debug.Log("half rewards");
            if (showresult == ShowResult.Failed) Debug.Log("No rewards");

        }
    }
}
