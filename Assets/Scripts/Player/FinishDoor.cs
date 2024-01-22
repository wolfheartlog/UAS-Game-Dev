using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoor : InteractScene
{
    private QuestManager questManager;
    private VIDE_Assign videAssign;
    private void Awake()
    {
        videAssign = GetComponent<VIDE_Assign>();
        questManager = FindObjectOfType<QuestManager>();
        if (questManager == null)
        {
            Debug.LogError("QuestManager not found!");
        }
    }

    public void FinishGameScene()
    {
            GameEventsManager.instance.levelEvents.LevelLoad(sceneName, spawnLocation);
    }

    public override void Update()
    {
        Quest backToHomeQuest = questManager.GetQuestById("BackToHome");
        if (backToHomeQuest != null && backToHomeQuest.state == QuestState.CAN_FINISH)
        {
            this.gameObject.layer = LayerMask.NameToLayer("InteractLayer");
            videAssign.enabled = true;
        }
    }
}
