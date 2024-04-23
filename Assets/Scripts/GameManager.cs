using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _soundOnObject;
    [SerializeField] GameObject _mapObject;
    [SerializeField] GameObject _gameOverObject;
    [SerializeField] GameObject _timeUpObject;
    [SerializeField] GameObject _winObject;
    [SerializeField] GameObject _resetObject;
    bool _gameWon = false;
    bool _gameLost = false;
    private void Start()
    {
        StartCoroutine(_startRoutine());    
    }

    IEnumerator _startRoutine()
    {
        _soundOnObject.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        _soundOnObject.SetActive(false);
        _mapObject.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        _mapObject.SetActive(false);
    }

    private void OnDisable()
    {
        TimePanelController.OnTimeUp -= TimePanelController_OnTimeUp;
        HealthPanelController.OnHealthDeplited -= HealthPanelController_OnHealthDeplited;
        RezaController.OnGameWon -= RezaController_OnGameWon;
    }
    private void OnEnable()
    {
        TimePanelController.OnTimeUp += TimePanelController_OnTimeUp;
        HealthPanelController.OnHealthDeplited += HealthPanelController_OnHealthDeplited;
        RezaController.OnGameWon += RezaController_OnGameWon;
    }

    private void RezaController_OnGameWon()
    {
        if (!_gameLost && !_gameLost)
        {
            _gameWon = true;
            _winObject.SetActive(true);
            _resetObject.SetActive(true);
        }
    }

    private void HealthPanelController_OnHealthDeplited()
    {
        if (!_gameWon && !_gameLost)
        {
            _gameOverObject.SetActive(true);
            _resetObject.SetActive(true);
            _gameLost = true;
        }
    }

    private void TimePanelController_OnTimeUp()
    {
        if (!_gameWon && !_gameLost)
        {
            _timeUpObject.SetActive(true);
            _resetObject.SetActive(true);
            _gameLost = true;
        }
    }
    public void _ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
