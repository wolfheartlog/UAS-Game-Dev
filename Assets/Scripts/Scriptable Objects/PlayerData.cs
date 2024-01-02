using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    public Vector2 spawnLocation = new Vector2(0,0);
    public int expToLevelUp = 100;
    public int currentLevel = 1;
}
