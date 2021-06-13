using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GameSettings
{
    public bool CutsceneOnly;
    public string[] CutsceneTexts;
    public int MinValue;
    public int MaxValue;
    public int InitialNumberCount;
    public float NumberSpawnRate;
    public int Goal;
    public bool FixedGoal;
    public bool CanSpawnNumbers;
    public int ErasersCount;
    public float EraserSpawnRate;
    public AudioClip CutsceneMusic;
    public AudioClip LvlMusic;
    public Vector2 levelSize;
}
