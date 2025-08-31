using UnityEngine.Advertisements;
using UnityEngine;

public class AdvertismentManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _AndroidGameId;
    [SerializeField] string _IOSGameId;
    string _gameId,_currentAdID;
    [SerializeField] string _adUnitId = "Rewarded_Android"; // Replace with your actual Ad Unit ID
    public static AdvertismentManager _Instance { get; private set; }
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
#if UNITY_ANDROID
        _gameId = _AndroidGameId;
#elif UNITY_IOS
                gameId = _IOSGameId;

#else
        gameId = _AndroidGameId;
#endif
        Advertisement.Initialize(_gameId, false, this);
        DontDestroyOnLoad(this.gameObject);


    }

    public void _ShowAd()
    {
        Advertisement.Show(_currentAdID, _Instance);
    }
    public void OnInitializationComplete()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string iPlacementId)
    {
        _currentAdID = iPlacementId;
        //Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Unity Ads Failed to Load: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Advertisement.Load(_adUnitId, this);
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Advertisement.Load(_adUnitId, this);
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    
}
