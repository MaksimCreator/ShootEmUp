using ShootEmUp.Model;
using ShootEmUp.Observer;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class GamaplayViewPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Button _exit;

    private PlayerData _playerData;
    private SingelPlayerData _singelPlayerData;
    private ICharacterObserver _characterObserver;
    private bool _isInit;

    public void Init(PlayerData playerData, ICharacterObserver characterObserver)
    {
        if (_isInit)
            return;

        _isInit = true;

        _singelPlayerData = SingelServiceLocator.GetService<SingelPlayerData>();
        _playerData = playerData;
        _characterObserver = characterObserver;

        _score.text = _singelPlayerData.AccamulatedScore.ToString();
        _healthBar.maxValue = _playerData.MaxHealth;
        _healthBar.minValue = _playerData.MinHealth;
        _healthBar.value = _playerData.CurentHealth;

        _characterObserver.TryAddActionOnTakeDamage(OnTakeDamage);
        _characterObserver.TryAddActionOnUpdateWallet(OnUpdateWallet);
    }

    public void Show(Action exit)
    {
        _exit.onClick.AddListener(() => 
        {
            Hide();
            exit();
        });

        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        _exit.onClick.RemoveAllListeners();
    }

    private void OnTakeDamage()
    => _healthBar.value = _playerData.CurentHealth;

    private void OnUpdateWallet()
    => _score.text = _singelPlayerData.AccamulatedScore.ToString();
}