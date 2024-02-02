using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gameOverCanvas;

    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI wavesText;
    [SerializeField] float refreshTime = 0.2f;



    private bool isPaused;
    private int currentWave;

    public int CurrentWave { get => currentWave; set => currentWave = value; }

    private void Start()
    {
        StartCoroutine(UpdateUI());
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
        gameManager.OnStartGame += OnStartGame;
        gameManager.OnPauseResumeGame += OnPauseResumeGame;
        gameManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        gameManager.OnStartGame -= OnStartGame;
        gameManager.OnPauseResumeGame -= OnPauseResumeGame;
        gameManager.OnGameOver -= OnGameOver;
    }

    private void OnStartGame()
    {
        Time.timeScale = 1;
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

    private IEnumerator UpdateUI()
    {
        while (true)
        {
            coinsText.text = gameManager.TotalCoins.ToString();
            livesText.text = gameManager.TotalLives.ToString();
            levelText.text = (gameManager.CurrentLevelIndex + 1).ToString();
            wavesText.text = currentWave.ToString();
            yield return new WaitForSeconds(refreshTime);
        }
        
    }

}
