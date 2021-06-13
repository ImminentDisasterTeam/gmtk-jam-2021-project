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
    
    public void AnnounceLevel(string text) {
        levelAnnouncement.SetActive(true);
        textWriter.textField = levelAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }

    public void AnnounceOnLevel(string text) {
        onLevelAnnouncement.SetActive(true);
        textWriter.textField = onLevelAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }

    public void AnnounceDeath(string text) {
        deadAnnouncement.SetActive(true);
        textWriter.textField = deadAnnouncement.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }


    void Start()
    {
        textWriter = GetComponent<TextWriter>();
    }

    void Update()
    {
        
    }
}
