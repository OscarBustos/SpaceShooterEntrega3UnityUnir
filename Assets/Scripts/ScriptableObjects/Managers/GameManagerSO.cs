using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Managers/Game Manager")]
public class GameManagerSO : ScriptableObject
{
    [SerializeField] int currentLevelIndex;
    [SerializeField] LevelSO[] levels;

    private bool isGameOver;
    
    public LevelSO CurrentLevel { get => levels[currentLevelIndex]; }
    public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }
    public bool IsGameOver { get => isGameOver; }

    #region Events
    public event Action OnStartGame;
    public event Action OnGameOver;
    public event Action OnPauseResumeGame;
    #endregion

    #region Methods
    public void StartGame()
    {
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
