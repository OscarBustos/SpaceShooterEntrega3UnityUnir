using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName ="Managers/Game Manager")]
public class GameManagerSO : ScriptableObject
{
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private int totalCoins;
    [SerializeField] private int totalLives;
    [SerializeField] private int scorePercentageToPass;
    [SerializeField] private StoreItemsListSO storeList;
    private bool isGameOver;
    private int pointsByEnemy = 20; // This value can be changed, but for this prototype it would be hardcoded
    private int levelCoins;
    
    public LevelSO CurrentLevel { get => levels[currentLevelIndex]; }
    public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    public int TotalCoins { get => totalCoins; set => totalCoins = value; }
    public int LevelCoins { get => levelCoins; set => levelCoins = value; }
    public int TotalLives { get => totalLives; set => totalLives = value; }

    public int MaxScore {  get => (CurrentLevel.Waves * CurrentLevel.EnemiesByWave * pointsByEnemy * scorePercentageToPass) / 100; 
    }

    #region Events
    public event Action OnStartGame;
    public event Action OnGameOver;
    public event Action OnWin;
    public event Action OnPauseResumeGame;
    public event Action OnWaveChange;
    public event Action OnChangeLevel;
    public event Action OnReloadPlayerPrefs;
    #endregion

    #region Methods
    public void StartGame()
    {
        currentLevelIndex = 0;
        totalCoins = 0;
        levelCoins = 0;
        PlayerPrefs.SetInt("GameStarted", 1);
        PlayerPrefs.SetInt("Lives", 3);
        PlayerPrefs.SetString("Skin", "");
        PlayerPrefs.SetInt("CannonNumber", 0);
        storeList.MutableList = null;
        storeList.SetList();
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
    {   if(currentLevelIndex == levels.Length - 1)
        {
            OnWin?.Invoke();
        }
        else if (levelCoins >= MaxScore)
        {
            OnChangeLevel?.Invoke();
        }
        else
        {
            GameOver();
        }
    }

    public void ReloadPlayerPrefs()
    {
        OnReloadPlayerPrefs?.Invoke();
    }
    #endregion


}
