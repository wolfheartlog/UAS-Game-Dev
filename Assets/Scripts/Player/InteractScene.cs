using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Vector2 spawnLocation;
    [SerializeField] LevelManager levelManager;
    bool isTrigger = false;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isTrigger = false;
        }
    }

    private void Update(){
        if (isTrigger && Input.GetKeyDown(KeyCode.E)) {
            GameEventsManager.instance.levelEvents.LevelLoad(sceneName, spawnLocation);
        }
    }
}
