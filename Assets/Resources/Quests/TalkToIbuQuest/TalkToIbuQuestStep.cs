using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToIbuQuestStep : QuestStep
{
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.state == QuestState.REQUIREMENT_NOT_MET){
            
        }
    }

    protected override void SetQuestStepState(string state)
    {
        // tidak ada state yang diperlukan untuk finish quest ini
    }
}
