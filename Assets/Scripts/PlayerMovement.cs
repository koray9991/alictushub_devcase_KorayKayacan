using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;
     Animator anim;
    Vector3 mouseDown;
    public FloatingJoystick js;
    public float moveSpeed;
     enum AnimationState
    {
        idle,
        walk,
        death
    }
    AnimationState animState;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = Input.mousePosition;
        }
        if (Input.mousePosition != mouseDown)
        {
            rb.velocity = new Vector3(js.Horizontal * moveSpeed, rb.velocity.y, js.Vertical * moveSpeed);
            if (js.Horizontal != 0 || js.Vertical != 0)
            {
                if (rb.velocity != Vector3.zero)
                {

                    transform.rotation = Quaternion.LookRotation(rb.velocity);
                    if (animState != AnimationState.walk)
                    {
                        animState = AnimationState.walk;
                        anim.SetTrigger("Walk");
                    }
                }



            }
            if (js.Horizontal == 0 && js.Vertical == 0)
            {
                if (animState != AnimationState.idle)
                {
                    animState = AnimationState.idle;
                    anim.SetTrigger("Idle");
                }
            }
        }
    }
    public void Death()
    {
        anim.SetTrigger("Death");
        rb.isKinematic = true;
    }
}
