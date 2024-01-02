using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private SpriteRenderer visual;

    private void Awake() 
    {
        circleCollider = GetComponent<CircleCollider2D>();
        visual = GetComponent<SpriteRenderer>();
    }
    public void CollectLeaves() 
    {
        circleCollider.enabled = false;
        visual.gameObject.SetActive(false);
        GameEventsManager.instance.miscEvents.LeavesCollected();
    }
}
