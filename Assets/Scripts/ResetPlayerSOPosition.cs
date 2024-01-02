using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetPlayerSOPosition : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    void OnEnable() {
        playerData.spawnLocation = Vector2. zero;
        Destroy(this.gameObject);
    }   
}
