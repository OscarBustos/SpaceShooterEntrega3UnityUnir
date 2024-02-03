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
    [SerializeField] private int scorePercentageToPass;
    private bool isGameOver;
    private int pointsByEnemy = 20; // This value can be changed, but for this prototype it would be hardcoded
    private int levelCoins;
    
    public LevelSO CurrentLevel { get => levels[currentLevelIndex]; }
    public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }
    public bool IsGameOver { get => isGameOver; }

    public int TotalCoins { get => totalCoins; set => totalCoins = value; }
    public int LevelCoins { get => levelCoins; set => levelCoins = value; }
    public int TotalLives { get => totalLives; set => totalLives = value; }

    public int MaxScore {  get => (CurrentLevel.Waves * CurrentLevel.EnemiesByWave * pointsByEnemy * scorePercentageToPass) / 100; 
    }

    #region Events
    public event Action OnStartGame;
    public event Action OnGameOver;
    public event Action OnPauseResumeGame;
    public event Action OnWaveChange;
    public event Action OnChangeLevel;
    #endregion

    #region Methods
    public void StartGame()
    {
        currentLevelIndex = 0;
        totalCoins = 0;
        levelCoins = 0;
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

    public void ChangeWave()
    {
        OnWaveChange?.Invoke();
    }

    public void ChangeLevel()
    {
        int percentage = (levelCoins * 100) / MaxScore;
        if (percentage >= scorePercentageToPass)
        {
            levelCoins = 0;
            OnChangeLevel?.Invoke();
        }
        else
        {
            GameOver();
        }
        
    }
    #endregion
}
