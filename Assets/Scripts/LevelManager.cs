using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gameOverCanvas;

    private bool isPaused;
    private void OnEnable()
    {
        Time.timeScale = 1;
        gameManager.OnPauseResumeGame += OnPauseResumeGame;
        gameManager.OnGameOver += OnGameOver;
    }

    private void OnPauseResumeGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseCanvas.SetActive(isPaused);
        
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
    }

}
