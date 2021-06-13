using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

class TextWriter : MonoBehaviour {
    public Text textField;
    [SerializeField] float pauseTime;
    Coroutine _coro;

    string textToWrite;

    public Action<Char> writeLetter;
    public Action finishWriting;

    public bool FastFinish() {
        bool result = false;
        if (_coro != null) {
            StopCoroutine(_coro);
            result = true;
        }
        textField.text = textToWrite;
        return result;
    }

    public void Clear() {
        if (_coro != null) {
            StopCoroutine(_coro);
        }
        textField.text = "";
    }

    public void WriteText(string textToWrite, float? timeToWait = null) {
        this.textToWrite = textToWrite;
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
            writeLetter(textToWrite[i-1]);
            yield return new WaitForSeconds(timeToWait);
        }
        finishWriting();
    }
}