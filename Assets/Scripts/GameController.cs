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
    private int currentLine = 0;
    private bool isSpeaking;

    private Coroutine textCoro;

    private void Start()
    {
        levelController.SwitchLevel = SwitchLevel;
        StartTextCutscene();
    }
    void StartTextCutscene()
    {
        isSpeaking = true;
        cutsceneController.ShowCutsceneWindow();
        currentLine = -1;
        NextLine();
    }
    void NextLine()
    {
        if (currentLine + 1 >= gameSettings[level].CutsceneTexts.Length)
        {
            cutsceneController.HideCutsceneWindow();
            isSpeaking = false;
            StartLevel();
        }
        else
            cutsceneController.WriteText(gameSettings[level].CutsceneTexts[++currentLine]);
    }
    void Skip()
    {
        if (!cutsceneController.FastWrite())
            NextLine();
    }
    void StartLevel()
    {
        audioController.PlayClip(gameSettings[level].LvlMusic);
        levelController.InitializeLevel(gameSettings[level], level);
    }

    void SwitchLevel()
    {
        levelController.StopAllCoroutines();
        level += 1;
        StartLevel();
    }

    private void Update()
    {
        if (isSpeaking && Input.GetButtonDown("Skip"))
            Skip();
    }
}
