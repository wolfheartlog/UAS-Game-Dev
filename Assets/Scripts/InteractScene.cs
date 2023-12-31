using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScene : LevelManager
{
    [SerializeField] private string sceneName;
    bool isTrigger;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isTrigger = true;
        }
    }

    private void Update(){
        if (isTrigger && Input.GetKeyDown(KeyCode.E)) {
            LoadScene(sceneName);
        }
    }
}
