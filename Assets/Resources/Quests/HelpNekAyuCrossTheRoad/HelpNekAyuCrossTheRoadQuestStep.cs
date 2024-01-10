using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HelpNekAyuCrossTheRoadQuestStep : QuestStep
{
    private float _time;
    private Vector2 nekAyuPosition;
    protected override void SetQuestStepState(string state)
    {
        // tidak ada state yang diperlukan untuk finish quest ini
    }

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

        if (quest.state == QuestState.IN_PROGRESS)
        {
            StartCoroutine(Transition(true));
        }
        else if (quest.state == QuestState.CAN_FINISH)
        {   
            StartCoroutine(Transition(false));
        }
    }

    public IEnumerator Transition(bool fade)
    {
        _time = Time.deltaTime * 0.5f;
        GameObject blackScreen = GameObject.FindGameObjectWithTag("BlackScreen");
        Image blackScreenImage = blackScreen.GetComponent<Image>();
        
        if (fade)
        {
            for (float i = 0; i <= 1; i += _time)
            {
                blackScreenImage.color = new Color(255, 255, 255, i);
                yield return null;
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject nekAyu = GameObject.Find("Nek Ayu");
            nekAyu.transform.position = new Vector3(-18.67f , 15.11f);
            player.transform.position = new Vector3(nekAyu.transform.position.x + 1, nekAyu.transform.position.y);

            yield return new WaitForSeconds(2f);
            FinishQuestStep();
        }
        else
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
            {
                blackScreenImage.color = new Color(255, 255, 255, i);
            }
        }
    }

    
}
