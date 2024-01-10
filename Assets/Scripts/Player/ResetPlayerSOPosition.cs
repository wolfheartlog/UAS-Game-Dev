using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetPlayerSOPosition : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    void OnEnable() {
        playerData.spawnLocation = Vector2. zero;
        playerData.currentGoodness = 0;
        playerData.nekAyu = false;
        playerData.diana = false;
        playerData.jonas = false;
        playerData.maya = false;
        playerData.leon = false;
        playerData.buNina = false;
        PlayerPrefs.DeleteAll();
        Destroy(this.gameObject);
    }   
}
