using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class TextWriter : MonoBehaviour {
    [TextArea] [SerializeField] string text;
    [SerializeField] Text textField;
    [SerializeField] float pauseTime;
    [SerializeField] bool updateTrigger;
    bool _prevTriggerValue;
    Coroutine _coro;

    void OnValidate() {
        if (updateTrigger && !_prevTriggerValue) {
            WriteText(text);
        }

        _prevTriggerValue = updateTrigger;
    }

    public void Clear() {
        if (_coro != null) {
            StopCoroutine(_coro);
        }
        textField.text = "";
    }

    public void WriteText(string textToWrite, float? timeToWait = null) {
        if (_coro != null) {
            StopCoroutine(_coro);
        }
        _coro = StartCoroutine(WriteTextCoro(textToWrite, timeToWait ?? pauseTime));
    }

    IEnumerator WriteTextCoro(string textToWrite, float timeToWait) {
        textField.text = "";
        
        var textLength = textToWrite.Length;
        for (var i = 1; i <= textLength; i++) {
            textField.text = textToWrite.Substring(0, i);
            yield return new WaitForSeconds(timeToWait);
        }
    }
}