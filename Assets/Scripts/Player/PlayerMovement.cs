        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
            animator.SetFloat("Direction", 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
            animator.SetFloat("Direction", 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }

        dir.Normalize();
        animator.SetBool("isMoving", dir.magnitude > 0);

        GetComponent<Rigidbody2D>().velocity = speed * dir;
    }
}

