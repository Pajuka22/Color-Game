using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicJump : MechanicParent
{
    // Start is called before the first frame update
    public MovementController PlayerMovement;
    public float Height;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Activate()
    {
        PlayerMovement.jumpHeight.Add(Height);
        PlayerMovement.jumpSpeed.Clear();
        PlayerMovement.jumpSpeed.TrimExcess();
        PlayerMovement.setJumpSpeed(0, PlayerMovement.jumpHeight.Count);
        enabled = false;
    }
}
