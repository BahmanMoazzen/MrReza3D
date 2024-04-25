using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public void _StartGame(int iDifficultyLevel)
    {
        PlayerPrefs.SetInt("DL", iDifficultyLevel);

        BAHMANLoadingManager._INSTANCE._LoadScene((int)GameScenes.Loader);
    }
}
