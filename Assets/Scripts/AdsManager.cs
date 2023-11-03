using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    [SerializeField] string gameID = "5466929", rewardedAdID = "Rewarded_Android";
    [SerializeField] private JsonSaveGameManager saveGameManager;

    void Awake()
    {
        saveGameManager = FindObjectOfType<JsonSaveGameManager>();

        if (saveGameManager == null)
        {
            Debug.LogError("JsonSaveGameManager no encontrado en la escena.");
        }
    }

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

    public void OnUnityAdsReady(string placementId)
    {
        // throw new System.NotImplementedException();
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

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewardedAdID)
        {

            if (showResult == ShowResult.Finished)
            {
                Debug.Log("Full rewards");
                SeeAdd();
            }
            else if (showResult == ShowResult.Skipped)
            {
                Debug.Log("half rewards");
                SkipAdd();
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.Log("No rewards");
            } 
        }
    }

    void SkipAdd()
    {
        saveGameManager.saveData.currencyKey += 50;
    }

    void SeeAdd()
    {
        saveGameManager.saveData.currencyKey += 100;
    }

}
