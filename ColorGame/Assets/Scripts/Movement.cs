using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    float jump;
    public int jumpBufferFrames = 3;
    bool crouch = false;
    public float speed = 0;
    public MovementController controller;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!MenuController.IsPaused)
        {
            speed = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                jump = jumpBufferFrames;
            }
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
	}
    private void FixedUpdate()
    {
        if (!MenuController.IsPaused)
        {
            controller.Move(speed, controller.bGrounded && jump > 0, crouch);
            if (controller.bGrounded && jump > 0)
            {
                jump = 0;
            }
            jump--;
        }
    }
}
