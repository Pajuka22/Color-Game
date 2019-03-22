using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicWalker : MechanicEnemy
{
    public enum States { NotWorking, Walking, Running, Transforming, Attacking}
    public GameObject player;
    public States currentState = States.NotWorking;
    public Rigidbody2D RB;
    public LayerMask enemyMask;
    float width;
    Transform myTransform;
    bool grounded;
    public float chaseRad = 100;
    public float distance;
    public Collider2D thisCollider;
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
        switch (currentState)
        {
            case States.Walking:
                {
                    if ((player.transform.position - this.transform.position).magnitude < chaseRad)
                    {
                        currentState = States.Transforming;
                    }
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
    public override void Activate()
    {
        WOKE = true;
        currentState = States.Walking;
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
    public void OnCollisionEnter2D(Collision2D col)
    {
        
    }
}
