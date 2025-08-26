using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{


    private void Start()
    {
        AnalyticsManager._TitleScreenLoaded();
    }
    public void _StartGame(int iDifficultyLevel)
    {
        PlayerPrefs.SetInt("DL", iDifficultyLevel);
        switch (iDifficultyLevel)
        {
            case 0:
                AnalyticsManager._EasySelected();
                break;
            case 1:
                AnalyticsManager._NormalSelected();
                break;
            case 2:
                AnalyticsManager._HardSelected();
                break;
            case 3:
                AnalyticsManager._InsaneSelected();
                break;
            
        }

        BAHMANLoadingManager._INSTANCE._LoadScene((int)GameScenes.MainGame);
    }
    public void _OtherPriducts()
    {
        BAHMANPublicRelation._Instance._OtherProductClicked();
    }
    public void _RateUs()
    {
        BAHMANPublicRelation._Instance._RateClicked();
    }
}
