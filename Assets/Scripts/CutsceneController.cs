using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    TextWriter textWriter;
    [SerializeField] GameObject cutsceneWindow;

    public void ShowCutsceneWindow() {
        cutsceneWindow.SetActive(true);
    }

    public void HideCutsceneWindow() {
        cutsceneWindow.SetActive(false);
    }

    public void WriteText(string text) {
        textWriter.textField = cutsceneWindow.GetComponentInChildren<Text>();
        textWriter.WriteText(text);
    }

    public bool FastWrite() {
        return textWriter.FastFinish();
    }

    void Start()
    {
        textWriter = GetComponent<TextWriter>();
    }
}
