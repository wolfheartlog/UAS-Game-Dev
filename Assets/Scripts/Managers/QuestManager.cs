using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;
    private int currentPlayerLevel;
    private void Awake(){
        questMap = createQuestMap();
    }

    private void OnEnable() {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;

        GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
    }

    private void OnDisable() {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;

        GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
    }

    private void PlayerLevelChange(int level){
        currentPlayerLevel = level;
    }

    private bool CheckRequirementsMet(Quest quest){
        bool meetsRequirements = true;

        if(currentPlayerLevel < quest.info.levelRequirements){
            meetsRequirements = false;
        }

        foreach(QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites){
            if(GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED){
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void Update(){
        foreach(Quest quest in questMap.Values){
            if(quest.state == QuestState.REQUIREMENT_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void Start() {
        foreach(Quest quest in questMap.Values){
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState state){
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }


    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();

        if(quest.CurrentStepExists()){
            quest.InstantiateCurrentQuestStep(this.transform);
        }else{
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest){
        GameEventsManager.instance.playerEvents.ExperienceGained(quest.info.experienceReward);
    }

    private Dictionary<string, Quest> createQuestMap(){
        
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach(QuestInfoSO questInfo in allQuests){
            if(idToQuestMap.ContainsKey(questInfo.id)){
                Debug.LogWarning("Quest dengan id "+questInfo.id+" sudah ada");
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }

        return idToQuestMap;
    }

    private Quest GetQuestById(string id){
        Quest quest = questMap[id];
        if(quest == null){
            Debug.LogError("ID "+id+" tidak ditemukan pada quest map");
        }
        return quest;

    }
}
