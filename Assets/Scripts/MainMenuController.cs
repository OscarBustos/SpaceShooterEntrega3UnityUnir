using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameManagerSO gameManager;
    [SerializeField] Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("GameStarted") == 1 ? true : false)
        {
            continueButton.gameObject.SetActive(true);
        } 
        else
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
