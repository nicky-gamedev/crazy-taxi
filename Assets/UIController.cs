using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private SpaceshipMovement _movement;

    [Header("UI Elements")] 
    [SerializeField] private Image _fuelBar;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private GameObject _gameOverPanel;

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(OpenGameOverScreen);
    }

    private void OpenGameOverScreen()
    {
        _gameOverPanel.SetActive(true);
    }

    void Update()
    {
        UpdateTimer();
        UpdateFuelBar();
    }

    public void UpdateTimer()
    {
        int val = GameManager.Instance.TimeRemaining;
        TimeSpan timeSpan = TimeSpan.FromSeconds(val);
        _timer.text = timeSpan.ToString(@"mm\:ss");
    }

    public void UpdateFuelBar()
    {
        _fuelBar.fillAmount = (float)_movement.fuel / _movement.maxFuel;
    }
}
