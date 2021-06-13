using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GameSettings
{
    public string[] CutsceneTexts;
    public int MinValue;
    public int MaxValue;
    public float NumberSpawnRate;
    public int Goal;
    public int ErasersCount;
    public float EraserSpawnRate;
    public AudioClip CutsceneMusic;
    public AudioClip LvlMusic;
}
