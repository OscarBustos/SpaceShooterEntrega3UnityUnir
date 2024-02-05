using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] Button continueButton;

    void Start()
    {
        if(PlayerPrefs.GetInt("GameStarted") == 1)
        {
            continueButton.gameObject.SetActive(true);
        } 
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }
}
