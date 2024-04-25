using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PublicRelationType { Share, Rate, OtherProduct, Donate ,Message}
[Serializable]
public struct PublicRelationSetting
{
    public string SceneName;
    public PublicRelationType RelationType;
    public int SceneCount;

}
[Serializable]
public struct PublicRelationMessageBoxInfo
{
    public string Title;
    public string Message;
}

public class BAHMANPublicRelation : MonoBehaviour
{
    const string PRPREFIX = "BPR";

    [Header("Global Settings")]
    [SerializeField] Text _messageBoxTitle;
    [SerializeField] Text _messageBoxDesc;
    [SerializeField] Button _messageBoxYes;
    [SerializeField] GameObject _messageBox;
    [SerializeField] PublicRelationMessageBoxInfo[] _PRTitle;
    [SerializeField] PublicRelationSetting[] _AllSettings;
    [Header("Share Settings")]
    [Tooltip("sets the subject (primarily used in e-mail applications)")]
    [SerializeField] string _subject;
    [Tooltip("sets the shared text. Note that the Facebook app will omit text, if exists")]
    [SerializeField] string _text;
    [Tooltip("sets the title of the share dialog on Android platform.Has no effect on iOS")]
    [SerializeField] string _title;

    [Header("Rating Settings")]
    [SerializeField]
    string _ratingURL;
    [Header("Other Products Settings")]
    [SerializeField]
    string _otherProductURL;
    [Header("Donate Settings")]
    [SerializeField]
    string _donateURL;


    private void Awake()
    {

        DontDestroyOnLoad(this);
        _ClosePanel();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
    private void SceneManager_sceneLoaded(Scene iLoadedScene, LoadSceneMode iLoadMode)
    {
        StopAllCoroutines();
        StartCoroutine(_scenLoadRoutine(iLoadedScene.name));
    }
    IEnumerator _scenLoadRoutine(string iSceneName)
    {
        string saveName = $"{PRPREFIX}_{iSceneName}_count";
        int newCount = PlayerPrefs.GetInt(saveName, 0) + 1;
        PlayerPrefs.SetInt(saveName, newCount);
        foreach (var prs in _AllSettings)
        {
            if (prs.SceneName.Equals(iSceneName))
            {
                if (newCount == prs.SceneCount)
                {
                    _messageBoxTitle.text = _PRTitle[(int)prs.RelationType].Title;
                    _messageBoxDesc.text = _PRTitle[(int)prs.RelationType].Message;
                    _messageBox.SetActive(true);
                    _messageBoxYes.onClick.AddListener(_ClosePanel);
                    switch (prs.RelationType)
                    {
                        case PublicRelationType.Share:
                            _messageBoxYes.onClick.AddListener(_ShareClicked);
                            break;
                        case PublicRelationType.Rate:
                            _messageBoxYes.onClick.AddListener(_RateClicked);
                            break;
                        case PublicRelationType.OtherProduct:
                            _messageBoxYes.onClick.AddListener(_OtherProductClicked);
                            break;
                        case PublicRelationType.Donate:
                            _messageBoxYes.onClick.AddListener(_DonateClicked);
                            break;
                        case PublicRelationType.Message:
                            _messageBoxYes.onClick.AddListener(_MessageClicked);
                            break;
                    }
                    break;
                }

            }
            yield return null;
        }
    }
    public void _ShareClicked()
    {
        NativeShare NS = new NativeShare().SetSubject(_subject).SetText(string.Format(_text, Application.identifier)).SetTitle(_title);
        Debug.Log(string.Format(_text, Application.identifier));
        NS.Share();
        //_ClosePanel();
        _RemoveAllYesListeners();
        
    }
    public void _OtherProductClicked()
    {
        Application.OpenURL(string.Format(_otherProductURL, Application.identifier));
        Debug.Log(string.Format(_otherProductURL, Application.identifier));
        //_ClosePanel();
        _RemoveAllYesListeners();

    }
    public void _RateClicked()
    {
        Application.OpenURL(string.Format( _ratingURL,Application.identifier));
        string.Format(string.Format(_ratingURL, Application.identifier));
        //_ClosePanel();
        _RemoveAllYesListeners();
    }
    public void _DonateClicked()
    {
        Application.OpenURL(_donateURL);
        string.Format(_donateURL);
        //_ClosePanel();
        _RemoveAllYesListeners();
    }
    public void _MessageClicked()
    {
        //Application.OpenURL(_donateURL);
        //_ClosePanel();
        _RemoveAllYesListeners();
    }
    public void _ClosePanel()
    {
        _messageBox.SetActive(false);
    }
    public void _RemoveAllYesListeners()
    {
        _messageBoxYes.onClick.RemoveAllListeners();
        
    }



}
