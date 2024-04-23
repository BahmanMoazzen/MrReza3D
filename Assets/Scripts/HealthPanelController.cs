using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthPanelController : MonoBehaviour
{
    public static event UnityAction OnHealthDeplited;
    public static event UnityAction<HealthPanelController> OnHealthBarReady;
    Slider _healthSlider;
    [SerializeField] float[] _reductionIntervals = { 1, .7f, .5f, .3f };
    [SerializeField] float _reductionAmount = .01f;
    [SerializeField] float _pumpAmount = .1f;
    [SerializeField] float _maxHealth = 1f;
    float _currentHealth;

    private void Start()
    {
        _healthSlider = GetComponentInChildren<Slider>();
        _currentHealth = _maxHealth;
        StartCoroutine(_reduceRoutine());
        OnHealthBarReady?.Invoke(this);
    }
    IEnumerator _reduceRoutine()
    {
        while (_currentHealth > 0)
        {
            yield return new WaitForSeconds(_reductionIntervals[PlayerPrefs.GetInt("DL", 0)]);
            _currentHealth -= _reductionAmount;
            _healthSlider.value = _currentHealth;
        }
        OnHealthDeplited?.Invoke();

    }
    public void _PumpHealth()
    {
        _currentHealth = Mathf.Min(_currentHealth += _pumpAmount, _maxHealth);
    }
}
