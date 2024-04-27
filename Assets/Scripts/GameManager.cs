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
    [SerializeField] TimePanelController _timePanelController;
    [SerializeField] HealthPanelController _healthPanelController;
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
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("به بازی آقارضا خوش آمدید!",Color.white,1.5f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("توی این بازی شما باید آقارضا رو بیدار کنید و نذارید به خواب مرگ فرو بره!",Color.white,3f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("باید دائم صداش کنید!",Color.white,1.5f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("در حالی که صداش می کنید باید توی آپارتمان بگردی و پیداش کنی تا برنده بازی بشی.",Color.white,3f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("زمانت محدوده، پس دست بجنبون.",Color.white,1.5f);
        BAHMANMessageBoxManager._INSTANCE._ShowMessage("تو پیدا کردنش موفق باشی.",Color.white,1f);
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
    /// <summary>
    /// detects the view chaneg and show or hide the reset view button
    /// </summary>
    /// <param name="iIsIdentity">determine whether the view is identity or not</param>
    private void ThirdPersonMovementController_OnViewChanged(bool iIsIdentity)
    {
        _resetView.SetActive(!iIsIdentity);
    }

    private void RezaController_OnGameWon()
    {
        if (!_gameWon && !_gameLost)
        {
            _gameWon = true;
            _winObject.SetActive(true);
            _resetObject.SetActive(true);
            _timePanelController.enabled = _healthPanelController.enabled = false;
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("آقا رضا بیدار شد!", Color.white, 1.5f);
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("شما برنده شدید!", Color.white, 1.5f);
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("اگه دوست داشتی سختی های دیگه رو امتحان کن", Color.white, 2.5f);
        }
    }

    private void HealthPanelController_OnHealthDeplited()
    {
        if (!_gameWon && !_gameLost)
        {
            _gameOverObject.SetActive(true);
            _resetObject.SetActive(true);
            _gameLost = true;
            _timePanelController.enabled = _healthPanelController.enabled = false;
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("متأسفانه آقا رضا مرد!", Color.white, 2f);
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("شما باختید!", Color.white, 1.5f);
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("دوباره امتحان کن", Color.white, 2.5f);
            
        }
    }

    private void TimePanelController_OnTimeUp()
    {
        if (!_gameWon && !_gameLost)
        {
            _timeUpObject.SetActive(true);
            _resetObject.SetActive(true);
            _gameLost = true;
            _timePanelController.enabled = _healthPanelController.enabled = false;
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("زمانت به پایان رسید!", Color.white, 2f);
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("شما باختید!", Color.white, 1.5f);
            BAHMANMessageBoxManager._INSTANCE._ShowMessage("دوباره امتحان کن", Color.white, 2.5f);
        }
    }
    public void _ResetGame()
    {
        BAHMANLoadingManager._INSTANCE._LoadScene((int)GameScenes.TitleScreen);
    }
}
