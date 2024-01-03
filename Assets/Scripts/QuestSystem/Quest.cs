using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest{

    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENT_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length];
        for(int i = 0; i < questStepStates.Length; i++){
            questStepStates[i] = new QuestStepState();
        }
    }
    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.state = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        if(this.questStepStates.Length != this.info.questStepPrefabs.Length){
            Debug.LogWarning("Quest step state tidak sesuai dengan quest step prefab, quest saat ini "+ info.id+" tolong reset data quest ini");
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < info.questStepPrefabs.Length;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform){
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if(questStepPrefab != null){
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }

    private GameObject GetCurrentQuestStepPrefab(){
        GameObject questStepPrefab = null;
        if(CurrentStepExists()){
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }else{
            Debug.LogWarning("Quest step prefab tidak ditemukan, quest saat ini "+ info.id +", pada indeks ke-"+currentQuestStepIndex);
        }
        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex){
        if(stepIndex < questStepStates.Length){
            questStepStates[stepIndex].state = questStepState.state;
        }else{
            Debug.LogWarning("Quest step state tidak dapat disimpan, quest saat ini "+ info.id +", pada indeks ke-"+stepIndex);
        }
    }

    public QuestData GetQuestData(){
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }
}
