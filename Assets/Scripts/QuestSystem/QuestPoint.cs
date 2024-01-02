using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{
    [SerializeField] QuestInfoSO questInfoForPoint;
    private string questId;
    protected QuestState currentQuestState;
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    // icon
    [SerializeField] private GameObject interactUI;
    private QuestIcon questIcon;

    private void Awake() {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable() {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable() {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    public virtual void QuestStateChange(Quest quest){
        if(quest.info.id.Equals(questId)){
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
        }

        if(currentQuestState == QuestState.CAN_FINISH){
            this.gameObject.layer = LayerMask.NameToLayer("InteractLayer");
        }
    }

    public void StartQuestInteract(){
        if(currentQuestState.Equals(QuestState.CAN_START) && startPoint){
            GameEventsManager.instance.questEvents.StartQuest(questId);
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            interactUI.SetActive(false);
        }
    }

    public void FinishQuestInteract(){
        if(currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint){
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
    }
    
}
