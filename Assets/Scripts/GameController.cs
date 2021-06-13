using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameSettings[] gameSettings;
    [SerializeField] LevelController levelController;
    [SerializeField] AudioController audioController;
    [SerializeField] CutsceneController cutsceneController;
    private int level = 0;
    private bool isSpeaking;

    void StartLevel()
    {
        audioController.PlayClip(gameSettings[level].LvlMusic);
        levelController.InitializeLevel(gameSettings[level], level);
    }

    void StartTextCutscene()
    {
        isSpeaking = true;
        cutsceneController.ShowCutsceneWindow();

        string[] lines = gameSettings[level].CutsceneTexts;

        for (var i = 0; i < lines.Length; ++i)
            cutsceneController.WriteText(lines[i]);

        cutsceneController.HideCutsceneWindow();
        isSpeaking = false;
    }
    void StartLevelSequence()
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

    private void Update()
    {
        // if (isSpeaking && Input.GetButtonDown("Skip"))
        //     if(!cutsceneController.FastWrite())
        //         cutsceneController
    }
}
