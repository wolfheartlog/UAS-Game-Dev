using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanLeavesQuestStep : QuestStep
{
    
    private int leavesCollected = 0;
    private int leavesToCollect = 5;


    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onLeavesCollected += LeavesCollected;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onLeavesCollected -= LeavesCollected;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.state == QuestState.IN_PROGRESS)
            {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Leaves");

            foreach (GameObject obj in objectsWithTag)
            {
                obj.layer = LayerMask.NameToLayer("InteractLayer");
            }
        }
    }

    private void LeavesCollected()
    {
        if (leavesCollected < leavesToCollect)
        {
            leavesCollected++;
        }

        if(leavesCollected >= leavesToCollect)
        {
            FinishQuestStep();
        }
    }
}