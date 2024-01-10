using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    public Vector2 spawnLocation = new Vector2(0,0);
    // public int expToLevelUp = 100;
    [Header("Goodness amount")]
    public int currentGoodness = 0;
    // npc goodness interactions
    [Header("Good Interactions to NPC")]
    public bool nekAyu = true;
    public bool diana = true;
    public bool jonas = true;
    public bool maya = true;
    public bool leon = false;
    public bool buNina = true;
    public int currentLevel = 1;
}
