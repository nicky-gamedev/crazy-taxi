using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int _startTime;
    [SerializeField] private MissionManager _missionManager;
    public int TimeRemaining { get; private set; }

    void Start()
    {
        SetupNewGame();
    }

    private void SetupNewGame()
    {
        AddTime(_startTime);
        StartCoroutine(UpdateTimer());
        _missionManager.GetNewDestination();
    }

    public void AddTime(int amount)
    {
        TimeRemaining = amount;
    }

    private IEnumerator UpdateTimer()
    {
        while (TimeRemaining > 0)
        {
            TimeRemaining -= 1;
            yield return new WaitForSeconds(1);
        }
        GameOver();
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    protected override void OnApplicationQuitCallback()
    {
        
    }

    protected override void OnEnableCallback()
    {
        
    }
}