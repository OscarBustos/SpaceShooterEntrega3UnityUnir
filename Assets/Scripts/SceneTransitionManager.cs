using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    
    public void NewGame()
    {
        gameManager.StartGame();
        LoadFirstScene();
    }

    private void LoadFirstScene()
    {
        LoadScene(gameManager.CurrentLevelIndex + 1);
    }

    public void ContinueGame()
    {
        LoadScene(gameManager.CurrentLevelIndex + 1);
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

}
