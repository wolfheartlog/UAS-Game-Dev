using UnityEngine;

public class InteractColliderUI : MonoBehaviour
{
    private Camera cam;
    private Vector3 offset = new Vector3(0, 1.5f, 0);
    [SerializeField] private GameObject interactUI;

    [SerializeField] private LayerMask interactLayerMask;
    private Transform target;

    private bool isColliding = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((interactLayerMask.value & 1 << other.gameObject.layer) != 0)
        {
            if (!interactUI.activeSelf)
            {
                interactUI.SetActive(true);
                target = other.gameObject.transform;
                MoveUI(target);
            }

            isColliding = true;
        }
        Debug.Log("Stay");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isColliding && (interactLayerMask.value & 1 << other.gameObject.layer) != 0)
        {
            if (interactUI.activeSelf)
            {
                interactUI.SetActive(false);
            }

            isColliding = false;
            Debug.Log("Exit");
        }
    }

    private void MoveUI(Transform obj)
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        Vector3 temp_pos = cam.WorldToScreenPoint(obj.position + offset);

        Canvas copyOfMainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        float scaleFactor = copyOfMainCanvas.scaleFactor;

        Vector3 pos = new Vector3(temp_pos.x / scaleFactor, temp_pos.y / scaleFactor, temp_pos.z);

        if (interactUI.transform.position != pos)
        {
            interactUI.transform.position = pos;
        }
        
        Debug.Log(obj.position);
    }

    private void Update()
    {
        if (interactUI.activeSelf)
        {
            MoveUI(target);
        }
    }
}
