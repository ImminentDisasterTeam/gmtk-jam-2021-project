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
    private bool isDead;

    private void Start()
    {
        levelController.SwitchLevel = SwitchLevel;
        levelController.Death = OnPlayerDeath;
        StartTextCutscene();
    }
    private void OnPlayerDeath()
    {
        audioController.StopMusic();
        isDead = true;
    }
    void StartTextCutscene()
    {
        audioController.PlayClip(gameSettings[level].CutsceneMusic);
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
            if (!gameSettings[level].CutsceneOnly)
                StartLevel();
            else
                EndGame();
        }
        else
            cutsceneController.WriteText(gameSettings[level].CutsceneTexts[++currentLine]);
    }
    void Skip()
    {
        if (!cutsceneController.FastWrite())
            if (isDead)
                Restart();
            else
                NextLine();
    }

    void EndGame()
    {
        levelController.StopAllCoroutines();
        //TODO: go to main menu
    }
    void Restart()
    {
        isDead = false;
        levelController.StopAllCoroutines();
        StartLevel();
    }
    void StartLevel()
    {
        audioController.PlayClip(gameSettings[level].LvlMusic);
        levelController.InitializeLevel(gameSettings[level], level);
    }

    void SwitchLevel()
    {
        level += 1;
        levelController.StopAllCoroutines();
        StartTextCutscene();
    }

    private void Update()
    {
        if (isSpeaking && Input.GetButtonDown("Skip"))
            Skip();
        if (isDead && Input.GetButtonDown("Skip"))
            Skip();
    }
}
