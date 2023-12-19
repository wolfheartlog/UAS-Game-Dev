using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    private Camera cam;
    private InteractData interactData;
    void Start()
    {    
        cam = Camera.main;
    }
    
    void Update() {
         Vector3 pos = cam.WorldToScreenPoint(interactData.lookAt.position + interactData.offset);

        if(transform.position != pos)
        {
            transform.position = pos;
        }
    }
       

}
