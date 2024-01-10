using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoor : InteractScene
{
    public void FinishGameScene()
    {
        GameEventsManager.instance.levelEvents.LevelLoad(sceneName, spawnLocation);
    }
    public override void Update()
    {
        
    }
}
