using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameSettings[] gameSettings;
    [SerializeField] LevelController levelController;
    [SerializeField] AudioController audioController;
    private int level = 0;

    void StartLevel()
    {
        audioController.PlayClip(gameSettings[level].LvlMusic);
        levelController.InitializeLevel(gameSettings[level], level);
    }

    void StartTextCutscene()
    {

    }

    private void Start()
    {
        levelController.SwitchLevel += SwitchLevel;
        StartLevel();
    }

    void SwitchLevel()
    {
        levelController.StopAllCoroutines();
        level += 1;
        StartLevel();
    }
}
