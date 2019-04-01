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
    public float chaseRad = 100;
    public float distance;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        height = this.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform = this.transform;
        Vector2 linecastPos = (myTransform.position + myTransform.right * width - myTransform.up * height);
        //Debug.DrawLine(linecastPos + Vector2.down, linecastPos + Vector2.down + Vector2.down);
        switch (currentState)
        {
            case States.Walking:
                {
                    Debug.DrawLine(linecastPos, linecastPos + Vector2.down);
                    grounded = Physics2D.Linecast(linecastPos, linecastPos + Vector2.down, WhatIsGround);
                    if (grounded)
                    {
                        RB.velocity = RB.velocity.x >= 0 ? new Vector2(speed, 0) : new Vector2(-speed, 0);
                    }
                    else
                    {
                        RB.velocity = new Vector2(-RB.velocity.x, RB.velocity.y);
                        transform.Rotate(new Vector3(0, 180, 0));
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
                    RB.velocity = new Vector2 (10 * sign(player.transform.position.x - this.transform.position.x), RB.velocity.y);
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
