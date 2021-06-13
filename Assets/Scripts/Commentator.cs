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
    public void AnnounceLevel() {
        levelAnnouncement.SetActive(true);
        textWriter.textField = levelAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText("Level " + level + " goal - die");
    }
    public void Summ(int sum)
    {
        AnnounceOnLevel("You get " + sum);
    }

    public void EnemyAppear(int index)
    {
        AnnounceOnLevel(index + " enemy appeared!");
    }

    void AnnounceOnLevel(string text) {
        onLevelAnnouncement.SetActive(true);
        textWriter.textField = onLevelAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }

    void AnnounceDeath(string text) {
        deadAnnouncement.SetActive(true);
        textWriter.textField = deadAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }

    void InvokeHiding() {
        Invoke("HideWindow", timeToHide);
    }

    void HideWindow() {
        onLevelAnnouncement.SetActive(false);
    }

    void Awake() {
        textWriter = GetComponent<TextWriter>();
        textWriter.finishWriting = InvokeHiding;
    }
    void Update()
    {
        
    }
}
