using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int minScoreToShowSotre;
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject storeCanvas;
    [SerializeField] FadePanelController fadePanelController;
    [SerializeField] float waitSecondsToFadeOut;

    [SerializeField] TextMeshProUGUI levelBigText;
    [SerializeField] TextMeshProUGUI waveBigText;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI wavesText;
    [SerializeField] float refreshTime = 0.2f;



    private bool isPaused;
    private bool showStore;
    private int currentWave = 1;

    public int CurrentWave { get => currentWave; set => currentWave = value; }

    private void Start()
    {
        if (gameManager.TotalCoins >= minScoreToShowSotre)
        {
            showStore = true;
            gameManager.PauseResumeGame();
        }
        gameManager.LevelCoins = 0;
        StartCoroutine(UpdateUI());

    }

    private void OnEnable()
    {
        Time.timeScale = 1;
        gameManager.OnStartGame += OnStartGame;
        gameManager.OnPauseResumeGame += OnPauseResumeGame;
        gameManager.OnGameOver += OnGameOver;
        gameManager.OnWaveChange += OnWaveChange;
        gameManager.OnChangeLevel += OnChangeLevel;
        gameManager.OnWin += OnWin;
    }

    private void OnDisable()
    {
        gameManager.OnStartGame -= OnStartGame;
        gameManager.OnPauseResumeGame -= OnPauseResumeGame;
        gameManager.OnGameOver -= OnGameOver;
        gameManager.OnWaveChange -= OnWaveChange;
        gameManager.OnChangeLevel -= OnChangeLevel;
        gameManager.OnWin -= OnWin;
    }

    #region Methods
    private void OnStartGame()
    {
        Time.timeScale = 1;
    }
    private void OnPauseResumeGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        if (showStore)
        {
            showStore = false;
            storeCanvas.SetActive(true);
        }
        else
        {
            storeCanvas.SetActive(false);
            pauseCanvas.SetActive(isPaused);
        } 
            
        
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
    }

    private void OnWin()
    {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
    }

    private void OnWaveChange()
    {
        StartCoroutine(UpdateUIWhenWaveChanged());
    }

    private void OnChangeLevel()
    {
        StartCoroutine(FadeOut());
    }

    public void SetShowStore(bool showStore)
    {
        this.showStore = showStore;
    }

    #endregion

    #region Coroutines
    private IEnumerator UpdateUI()
    {
        while (true)
        {
            coinsText.text = gameManager.LevelCoins.ToString() + " / " + gameManager.MaxScore.ToString() ;
            livesText.text = gameManager.TotalLives.ToString();
            levelText.text = (gameManager.CurrentLevelIndex + 1).ToString();
            wavesText.text = currentWave.ToString();
            yield return new WaitForSeconds(refreshTime);
        }
        
    }

    private IEnumerator UpdateUIWhenWaveChanged()
    {
        levelBigText.text = "Level " + (gameManager.CurrentLevelIndex + 1);
        waveBigText.text = "Wave " + currentWave;
        yield return new WaitForSeconds(gameManager.CurrentLevel.TimeBetweenWaves);
        levelBigText.text = "";
        waveBigText.text = "";
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(waitSecondsToFadeOut);
        fadePanelController.FadeOut();
    }
    #endregion
}
