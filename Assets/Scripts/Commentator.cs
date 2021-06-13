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

    
    int level;

    public void SetLevel(int level) {
        this.level = level;
    } 
    private void AnnounceLevel() {
        levelAnnouncement.SetActive(true);
        textWriter.textField = levelAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText("Level " + level + " goal - die");
    }

    private void AnnounceOnLevel(string text) {
        onLevelAnnouncement.SetActive(true);
        textWriter.textField = onLevelAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }

    private void AnnounceDeath(string text) {
        deadAnnouncement.SetActive(true);
        textWriter.textField = deadAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }

    public void Summ(int sum) {
        AnnounceOnLevel("You get " + sum);
    }

    public void EnemyAppear(int index) {
        AnnounceOnLevel(index + " enemy appeared!");
    }

    private void InvokeHiding() {
        Invoke("HideWindow", timeToHide);
    }

    private void HideWindow() {
        levelAnnouncement.SetActive(false);
        onLevelAnnouncement.SetActive(false);
    }

    private void Awake() {
        textWriter = GetComponent<TextWriter>();
        textWriter.finishWriting = InvokeHiding;
    }

    void Update()
    {
        
    }
}
