using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicWalker : MechanicEnemy
{
    public enum States { NotWorking, Walking, Running, Transforming, Attacking}
    public GameObject player;
    public States currentState = States.NotWorking;
    public Rigidbody2D RB;
    public LayerMask WhatIsGround;
    float width;
    float height;
    Transform myTransform;
    bool grounded;
    public float jumpHeight = 30;
    float jumpSpeed;
    public float speed = 5;
    public float chaseSpeed;
    public float chaseRad = 100;
    bool canJump = false;
    // Start is called before the first frame update
    void Start()
    {
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        height = this.GetComponent<SpriteRenderer>().bounds.extents.y;
        jumpSpeed = Mathf.Sqrt(2 * RB.gravityScale * jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        myTransform = this.transform;
        Vector2 linecastPos = (myTransform.position + myTransform.right * width - myTransform.up * height);
        grounded = Physics2D.Linecast(linecastPos, linecastPos + Vector2.down, WhatIsGround);
        Debug.DrawLine(linecastPos, linecastPos + Vector2.down);
        switch (currentState)
        {
            case States.Walking:
                {
                    if (grounded)
                    {
                        RB.velocity = RB.velocity.x >= 0 ? new Vector2(speed, RB.velocity.y) : new Vector2(-speed, RB.velocity.y);
                    }
                    else
                    {
                        RB.velocity = new Vector2(-RB.velocity.x, RB.velocity.y);
                        transform.Rotate(new Vector3(0, 180, 0));
                    }
                    if((this.transform.position - player.transform.position).magnitude < chaseRad)
                    {
                        currentState = States.Transforming;
                    }
                }
                break;
            case States.Transforming:
                {
                    StartCoroutine(Transform(1));
                }
                break;
            case States.Running:
                {
                    RB.velocity = new Vector2(chaseSpeed * sign(player.transform.position.x - this.transform.position.x), RB.velocity.y);
                    if (grounded)
                    {
                        canJump = true;
                    }
                    else
                    {
                        if (canJump)
                        {
                            RB.velocity = new Vector2(RB.velocity.x, jumpSpeed);
                            canJump = false;
                        }
                    }
                    if ((player.transform.position - this.transform.position).magnitude > chaseRad)
                    {
                        currentState = States.Walking;
                    }
                }
                break;
            default:
                {
                    currentState = States.NotWorking;
                }
                break;
        }
    }
    public override void ActAwake()
    {
        
    }
    public int sign(float val)
    {
        if(val == 0)
        {
            return 0;
        }else if(val < 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
    IEnumerator Transform(float time)
    {
        RB.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(time);
        currentState = States.Running;
    }
    public override void Activate()
    {
        base.Activate();
        currentState = States.Walking;
    }
}
