using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public void _StartGame(int iDifficultyLevel)
    {
        PlayerPrefs.SetInt("DL", iDifficultyLevel);

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
