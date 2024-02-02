using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Managers/Game Manager")]
public class GameManagerSO : ScriptableObject
{
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private int totalCoins;
    [SerializeField] private int totalLives;
    private bool isGameOver;
    
    public LevelSO CurrentLevel { get => levels[currentLevelIndex]; }
    public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }
    public bool IsGameOver { get => isGameOver; }

    public int TotalCoins { get => totalCoins; set => totalCoins = value; }
    public int TotalLives { get => totalLives; set => totalLives = value; }

    #region Events
    public event Action OnStartGame;
    public event Action OnGameOver;
    public event Action OnPauseResumeGame;
    #endregion

    #region Methods
    public void StartGame()
    {
        currentLevelIndex = 0;
        totalCoins = 0;
        PlayerPrefs.SetInt("GameStarted", 1);
        OnStartGame?.Invoke();
    }

    public void GameOver()
    {
        isGameOver = true;
        PlayerPrefs.SetInt("GameStarted", 0);
        OnGameOver?.Invoke();
    }

    public void PauseResumeGame()
    {
        OnPauseResumeGame?.Invoke();
    }
    #endregion
}
