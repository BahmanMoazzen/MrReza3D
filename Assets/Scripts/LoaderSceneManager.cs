using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoaderSceneManager : MonoBehaviour
{
    [SerializeField] GameObject _LoaderPanel;
    [SerializeField] Text _percentageLoadedText;
    IEnumerator _loadRoutine()
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync((int)GameScenes.MainGame, LoadSceneMode.Additive);
        do
        {
            yield return 0;
            _percentageLoadedText.text = $"{ Mathf.Round(AO.progress * 100).ToString()} %";
        } while (!AO.isDone);
        
        _LoaderPanel.SetActive(false);
    }
    IEnumerator Start()
    {
        _LoaderPanel.SetActive(true);
        yield return 0;
        StartCoroutine(_loadRoutine());
        
    }
}
