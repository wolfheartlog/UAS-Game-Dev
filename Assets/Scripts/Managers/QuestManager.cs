using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private bool loadQuestState = true;
    private Dictionary<string, Quest> questMap;
    private int currentPlayerLevel;
    private void Awake(){
        questMap = createQuestMap();
    }

    private void OnEnable() {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.instance.levelEvents.onLevelLoad += SaveQuestOnLevelLoad;

        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;

        GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
    }

    private void OnDisable() {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.instance.levelEvents.onLevelLoad -= SaveQuestOnLevelLoad;

        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;

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
            if(quest.state == QuestState.IN_PROGRESS){
                quest.InstantiateCurrentQuestStep(this.transform);
            }

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

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState){
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> createQuestMap(){
        
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach(QuestInfoSO questInfo in allQuests){
            if(idToQuestMap.ContainsKey(questInfo.id)){
                Debug.LogWarning("Quest dengan id "+questInfo.id+" sudah ada");
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }

        return idToQuestMap;
    }

    public Quest GetQuestById(string id){
        Quest quest = questMap[id];
        if(quest == null){
            Debug.LogError("ID "+id+" tidak ditemukan pada quest map");
        }
        return quest;
    }

    private void OnApplicationQuit(){
        foreach(Quest quest in questMap.Values){
            SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest){
        try{
            QuestData questData = quest.GetQuestData();
            string serializedData = JsonUtility.ToJson(questData);
            PlayerPrefs.SetString(quest.info.id, serializedData);

            Debug.Log(serializedData);
        }catch(System.Exception e){
            Debug.LogError("Gagal menyimpan quest "+quest.info.id+" karena "+e.Message);
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;

        try{
            if(PlayerPrefs.HasKey(questInfo.id) && loadQuestState){
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }else{
                quest = new Quest(questInfo);
            }
        }catch(System.Exception e){
            Debug.LogError("Gagal memuat quest "+questInfo.id+" karena "+e.Message);
        }
        return quest;
    }

    private void SaveQuestOnLevelLoad(string sceneName, Vector2 pos){
        foreach(Quest quest in questMap.Values){
            SaveQuest(quest);
        }
    }
}
