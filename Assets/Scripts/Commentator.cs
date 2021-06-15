using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commentator : MonoBehaviour
{
    TextWriter textWriter;
    [SerializeField] GameObject levelAnnouncement;
    [SerializeField] GameObject onLevelAnnouncement;
    [SerializeField] GameObject deadAnnouncement;
    [SerializeField] float timeToHide = 2f;

    Coroutine _coro;
    
    int level;

    public void SetLevel(int level) {
        this.level = level + 1;
        onLevelAnnouncement.SetActive(false);
        deadAnnouncement.SetActive(false);
        AnnounceLevel();
        InitialSpeech();
    } 
    void AnnounceLevel() {
        levelAnnouncement.SetActive(true);
        switch (level) {
            case 1:
                levelAnnouncement.GetComponentInChildren<Text>().text ="Level " + level + " goal - 3";
                break;
            case 2:
                levelAnnouncement.GetComponentInChildren<Text>().text ="Level " + level + " goal - 13";
                break;
            case 3:
                levelAnnouncement.GetComponentInChildren<Text>().text ="Level " + level + " goal - more than 70";
                break;
            case 4:
                levelAnnouncement.GetComponentInChildren<Text>().text ="Level " + level + " goal - more than 500";
                break;
            case 5:
                levelAnnouncement.GetComponentInChildren<Text>().text ="Level " + level + " goal - 1000";
                break;
        }
        
    }
    public void Summ(int sum)
    {
        if (level == 1) {
            if (sum == 2)
                AnnounceOnLevel("You got 2! You can sum numbers by getting them in both hands. Now you need number 3!");
            else if (sum == 3) {
                AnnounceOnLevel("There is a storage that no one else can enter but you. Put the collected number there so that it is definitely protected from the enemy.");
            }
        }
    }

    public void EnemyAppear(int index)
    {
        if (level == 2)
            AnnounceOnLevel("Oh no, it's him! One of the Erasers is nearby! Be careful, Plus create 13 and don't die. New numbers will sometimes come to you on the field to help, but they may be erased.");
        else if (level == 3) 
            AnnounceOnLevel("Beware, there are more and more enemies!");
    }

    private void InitialSpeech() {
        if (level == 1) {
            AnnounceOnLevel("Use Q and E to take numbers.");
        }
        else if (level == 2) {
            AnnounceOnLevel("Now your task is to create number 13. The goal of the level is written above the field. Good luck, Plus!");
        }
        else if (level == 3) {
            AnnounceOnLevel("The Eraser walks nearby from the start! For now, create a number greater than 70.");
        }
    }
    public void AnnounceDeath()
    {
        onLevelAnnouncement.SetActive(false);
        levelAnnouncement.SetActive(false);
        deadAnnouncement.SetActive(true);
        textWriter.textField = deadAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText("The world is plunging into darkness. Erasers are rapidly absorbing numbers, soon we will all die ... Ah, if only there was an opportunity to go back and save, Plus!");
    }

    public void CloseAll() {
        onLevelAnnouncement.SetActive(false);
        levelAnnouncement.SetActive(false);
        deadAnnouncement.SetActive(false);
    }
    void AnnounceOnLevel(string text) {
        if (_coro != null) {
            StopCoroutine(_coro);
        }
        onLevelAnnouncement.SetActive(true);
        textWriter.textField = onLevelAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }

    void InvokeHiding() {
        _coro = StartCoroutine("HideWindow");
    }

    IEnumerator HideWindow() {
        yield return new WaitForSeconds(timeToHide);
        onLevelAnnouncement.SetActive(false);
    }

    void Awake() {
        textWriter = GetComponent<TextWriter>();
        textWriter.finishWriting = InvokeHiding;
    }
}
