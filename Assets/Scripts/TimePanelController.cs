using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimePanelController : MonoBehaviour
{
    public static event UnityAction OnTimeUp;
    Text _timeText;
    [SerializeField] int[] _maxTime = { 120, 100, 50, 30 };
    int _currentTime;
    void Start()
    {
        _timeText = GetComponentInChildren<Text>();
        _currentTime = _maxTime[PlayerPrefs.GetInt("DL", 0)];
        StartCoroutine(_tikRoutine());
    }
    string _timeToString(int iTime)
    {
        int min = (int)(((float)iTime) / 60f);
        int sec = iTime - (min * 60);
        return $"{min}:{sec}";
    }
    IEnumerator _tikRoutine()
    {
        do
        {
            
            _timeText.text = _timeToString(_currentTime);
            yield return new WaitForSeconds(1f);
            _currentTime--;
        } while (_currentTime>0);
        OnTimeUp?.Invoke();
    }
}
