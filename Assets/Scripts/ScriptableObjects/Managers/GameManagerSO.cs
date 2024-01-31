using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Managers/Game Manager")]
public class GameManagerSO : ScriptableObject
{
    [SerializeField] int currentLevelIndex;
    [SerializeField] LevelSO[] levels;

    private bool gameOver;
    
    public LevelSO CurrentLevel { get => levels[currentLevelIndex]; }


    public bool GameOver { get => gameOver; }
}
