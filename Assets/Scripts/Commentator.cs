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
        onLevelAnnouncement.SetActive(false);
        deadAnnouncement.SetActive(false);
        AnnounceLevel();
    } 
    void AnnounceLevel() {
        levelAnnouncement.SetActive(true);
        levelAnnouncement.GetComponentInChildren<Text>().text ="Level " + level + " goal - die";
    }
    public void Summ(int sum)
    {
        AnnounceOnLevel("You get " + sum);
    }

    public void EnemyAppear(int index)
    {
        AnnounceOnLevel(index + " enemy appeared!");
    }
    public void AnnounceDeath()
    {
        onLevelAnnouncement.SetActive(false);
        levelAnnouncement.SetActive(false);
        deadAnnouncement.SetActive(true);
        textWriter.textField = deadAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText("You died :( Press <Space> to restart!");
    }

    public void CloseAll() {
        onLevelAnnouncement.SetActive(false);
        levelAnnouncement.SetActive(false);
        deadAnnouncement.SetActive(false);
    }
    void AnnounceOnLevel(string text) {
        onLevelAnnouncement.SetActive(true);
        textWriter.textField = onLevelAnnouncement.GetComponentInChildren<Text>();
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
}
