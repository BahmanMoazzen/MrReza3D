using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _soundOnObject;
    [SerializeField] GameObject _mapObject;
    [SerializeField] GameObject _gameOverObject;
    [SerializeField] GameObject _timeUpObject;
    [SerializeField] GameObject _winObject;
    [SerializeField] GameObject _resetObject;
    [SerializeField] GameObject _resetView;
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
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("به بازی آقارضا خوش آمدید!",Color.white,2f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("توی این بازی شما باید آقارضا رو بیدار کنید و نذارید به خواب مرگ فرو بره!",Color.white,3.5f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("باید دائم صداش کنید!",Color.white,2f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("در حالی که صداش می کنید باید توی آپارتمان بگردی و پیداش کنی تا برنده بازی بشی.",Color.white,4f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("زمانت محدوده، پس دست بجنبون.",Color.white,2.5f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("تو پیدا کردنش موفق باشی.",Color.white,1.5f);
    }

    private void OnDisable()
    {
        TimePanelController.OnTimeUp -= TimePanelController_OnTimeUp;
        HealthPanelController.OnHealthDeplited -= HealthPanelController_OnHealthDeplited;
        RezaController.OnGameWon -= RezaController_OnGameWon;
        ThirdPersonMovementController.OnViewChanged -= ThirdPersonMovementController_OnViewChanged;
    }
    private void OnEnable()
    {
        ThirdPersonMovementController.OnViewChanged += ThirdPersonMovementController_OnViewChanged;
        TimePanelController.OnTimeUp += TimePanelController_OnTimeUp;
        HealthPanelController.OnHealthDeplited += HealthPanelController_OnHealthDeplited;
        RezaController.OnGameWon += RezaController_OnGameWon;
    }

    private void ThirdPersonMovementController_OnViewChanged(bool iIsIdentity)
    {
        _resetView.SetActive(!iIsIdentity);
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
        BAHMANLoadingManager._INSTANCE._LoadScene((int)GameScenes.TitleScreen);
    }
}
