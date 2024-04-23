using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void _StartGame(int iDifficultyLevel)
    {
        PlayerPrefs.SetInt("DL", iDifficultyLevel);

        SceneManager.LoadScene(1);
    }
}
