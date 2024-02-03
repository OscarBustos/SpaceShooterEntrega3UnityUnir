using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;

    private void OnEnable()
    {
        gameManager.OnChangeLevel += OnChangeLevel;
    }

    private void OnDisable()
    {
        gameManager.OnChangeLevel -= OnChangeLevel;
    }

    public void NewGame()
    {
        gameManager.StartGame();
        LoadFirstScene();
    }

    private void LoadFirstScene()
    {
        LoadScene(1);
    }

    public void ContinueGame()
    {
        gameManager.LevelCoins = 0;
        LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void BackToMainMenu()
    {
        LoadScene(0);
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnChangeLevel()
    {
        StartCoroutine(LoadSceneAfter(3));
    }

    private IEnumerator LoadSceneAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameManager.CurrentLevelIndex++;
        LoadScene(1);
    }
}
