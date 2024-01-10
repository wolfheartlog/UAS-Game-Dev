using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScene : MonoBehaviour
{
    [SerializeField] public string sceneName;
    [SerializeField] public Vector2 spawnLocation;
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

    public virtual void Update(){
        if (isTrigger && Input.GetKeyDown(KeyCode.E)) {
            GameEventsManager.instance.levelEvents.LevelLoad(sceneName, spawnLocation);
        }
    }
}
