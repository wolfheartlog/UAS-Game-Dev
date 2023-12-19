using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : ScriptableObject
{
    [Header("Information")]
    public string sceneName;
    public string shortDescription;

    [Header("Sounds")]
    public AudioClip music;
    [Range(0.0f, 1.0f)]
    public float musicVolume;
}
