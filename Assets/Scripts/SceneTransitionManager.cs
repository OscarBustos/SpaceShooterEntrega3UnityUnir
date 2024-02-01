using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    
    public void NewGame()
    {
        gameManager.CurrentLevelIndex = 0;
        gameManager.StartGame();
        Invoke("LoadFirstScene", 1f);
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

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
