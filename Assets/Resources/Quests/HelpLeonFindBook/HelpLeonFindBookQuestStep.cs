using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HelpLeonFindBookQuestStep : QuestStep
{
    private bool questActive = false;
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }
    protected override void SetQuestStepState(string state)
    {
        // tidak ada state yang diperlukan untuk finish quest ini
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && questActive){
            FinishQuestStep();
            Debug.Log("Quest Finished");
        }
    }

    private void QuestStateChange(Quest quest){
        if(quest.state == QuestState.IN_PROGRESS)
        {
            GameObject bookArea = GameObject.FindGameObjectWithTag("BookArea");
            questActive = true;
            bookArea.SetActive(true);
        }

        if(quest.state == QuestState.FINISHED)
        {
            GameObject bookArea = GameObject.FindGameObjectWithTag("BookArea");
            questActive = false;
            bookArea.SetActive(false);
        }
    }
}
