using UnityEngine;
using VIDE_Data;
public class QuestFinishConversation : QuestPoint
{
    [SerializeField] private int NodeID;
    public Template_UIManager diagUI;
    
    public override void QuestStateChange(Quest quest)
    {
        base.QuestStateChange(quest);
        if(currentQuestState == QuestState.CAN_FINISH){
            
        }
    }
}
