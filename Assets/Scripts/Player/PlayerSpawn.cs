using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    
    void Start() {    
        transform.position = playerData.spawnLocation;
    }

    
}
