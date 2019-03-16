using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicWalker : MechanicEnemy
{
    public enum States { NotWorking, Walking, Running, Transforming, Attacking}
    public States currentState = States.NotWorking;
    public Rigidbody2D RB;
    public LayerMask enemyMask;
    float width;
    Transform myTransform;
    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        myTransform = this.transform;
        Vector2 linecastPos = (myTransform.position - myTransform.right * width);
        Debug.DrawLine(linecastPos + Vector2.down, linecastPos + Vector2.down + Vector2.down);
        grounded = Physics2D.Linecast(linecastPos + Vector2.down, linecastPos + Vector2.down + Vector2.down, enemyMask);
        if (currentState.Equals(States.Walking))
        {
            if (!grounded)
            {
                Vector3 currRot = myTransform.eulerAngles;
                currRot.y += 180;
                this.transform.eulerAngles = currRot;
                if (RB.velocity.x > 0)
                {
                    RB.velocity = new Vector2(-8, RB.velocity.y);
                }
                else
                {
                    RB.velocity = new Vector2(8, RB.velocity.y);
                }
            }
            else
            {
                if (RB.velocity.x < 0)
                {
                    RB.velocity = new Vector2(-8, RB.velocity.y);
                }
                else
                {
                    RB.velocity = new Vector2(8, RB.velocity.y);
                }
            }
        }
    }
    public override void ActAwake()
    {

    }
    public void GoApeShit()
    {

    }
}
