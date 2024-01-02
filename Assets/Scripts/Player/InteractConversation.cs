using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class InteractConversation : InteractColliderUI
{
    public Template_UIManager diagUI;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private VIDE_Assign inTrigger;

    private LayerMask layerMask;

    private bool isActivated = false;

    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();    
        playerRB = GetComponent<Rigidbody2D>();    
        playerAnimator = GetComponent<Animator>();    
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
            layerMask = other.gameObject.layer;
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        inTrigger = null;
    }

    public override void Update()
    {
        base.Update();

        if (VD.isActive)
        {
            playerRB.velocity = 0 * Vector2.zero;
            playerAnimator.enabled = false;
            playerMovement.enabled = false;
        }else{
            isActivated = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && !isActivated && (interactLayerMask.value & 1 << layerMask) != 0)
        {
            TryInteract();
        }

    }
    void TryInteract()
    {
        isActivated = true;
        if (inTrigger)
        {
            diagUI.Interact(inTrigger);
            return;
        }

        RaycastHit rHit;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
        {
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            else return;

            if (assigned.alias == "QuestUI")
            {
                // questUI.Interact();
            } else
            {
                diagUI.Interact(assigned);
            }
        }
    }

}
