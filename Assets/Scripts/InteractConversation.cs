using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class InteractConversation : InteractColliderUI
{
    public string playerName = "Yuliana";
    public Template_UIManager diagUI;

    // public QuestChartDemo questUI;
    public PlayerMovement playerMovement;
    public VIDE_Assign inTrigger;

    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();    
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
            Debug.Log(inTrigger);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        inTrigger = null;
    }

    public override void Update()
    {
        base.Update();
        //Only allow player to move and turn if there are no dialogs loaded
        if (VD.isActive)
        {
            playerMovement.enabled = false;
        }

        //Interact with NPCs when pressing E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

    }
    void TryInteract()
    {
        /* Prioritize triggers */

        if (inTrigger)
        {
            diagUI.Interact(inTrigger);
            return;
        }

        /* If we are not in a trigger, try with raycasts */

        RaycastHit rHit;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
        {
            //Lets grab the NPC's VIDE_Assign script, if there's any
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            else return;

            if (assigned.alias == "QuestUI")
            {
                // questUI.Interact(); //Begins interaction with Quest Chart
            } else
            {
                diagUI.Interact(assigned); //Begins interaction
            }
        }
    }
}
