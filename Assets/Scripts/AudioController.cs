using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip speechSound;
    [SerializeField][Range(0,1)] float speechSoundVolume;
    [SerializeField] TextWriter textWriter;

    AudioSource audioSource;

    Coroutine _coro;

    void playSound(char symb) {
        if (Char.IsLetter(symb))
            audioSource.PlayOneShot(speechSound, speechSoundVolume);
    }

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        textWriter.writeLetter += playSound;
    }

    public void PlayClip(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopMusic() {
        audioSource.Stop();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
