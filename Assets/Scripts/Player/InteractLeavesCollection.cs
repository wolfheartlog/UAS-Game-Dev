using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLeavesCollection : MonoBehaviour
{
    Leaves leaves;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Leaves>() != null)
        {
            leaves = other.GetComponent<Leaves>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        leaves = null;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && leaves != null && leaves.gameObject.layer == LayerMask.NameToLayer("InteractLayer"))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        if (leaves != null)
        {
            leaves.CollectLeaves();
        }
    }
}
