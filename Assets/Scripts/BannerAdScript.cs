using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour
{

    public string gameId = "3488728";
    public string placementId = "bannerPlacement";
    public bool testMode = true;
    //public BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }        
        //Advertisement.Banner.SetPosition(bannerPosition);
        //Advertisement.Banner.Show(placementId);
    }
}