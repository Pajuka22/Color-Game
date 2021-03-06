﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Animator))]
public class MWalker : MEnemy
{
    public enum States { NotWorking, Walking, Running, Transforming, Attacking}
    public GameObject player;
    int vertices = 10;
    public States currentState = States.NotWorking;
    public Rigidbody2D RB;
    public LayerMask WhatIsGround;
    float width;
    float height;
    Transform myTransform;
    public bool grounded;
    public float jumpHeight = 30;
    float jumpSpeed;
    public float speed = 5;
    public float chaseSpeed = 8;
    public float chaseRad = 100;
    public float YEETrad = 50;
    public float YEETFORCE = 100;
    bool canJump = false;
    bool yeeted = false;
    float deltaTheta;
    private LineRenderer linerenderer;
    private Animator Anim;
    bool touchGround = false;
    Vector2 cacheVel;
    bool cachePause;
    // Start is called before the first frame update
    void Start()
    {
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        height = this.GetComponent<SpriteRenderer>().bounds.extents.y;
        jumpSpeed = Mathf.Sqrt(2 * RB.gravityScale * jumpHeight);
        linerenderer = GetComponent<LineRenderer>();
        deltaTheta = (2f * Mathf.PI) / vertices;
        Anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuController.IsPaused)
        {
            if (cachePause)
            {
                RB.velocity = cacheVel;
            }
            myTransform = this.transform;
            Vector2 linecastPos = (myTransform.position + myTransform.right * width - myTransform.up * height);
            grounded = Physics2D.Linecast(linecastPos, linecastPos + new Vector2(0f, -0.5f), WhatIsGround);

            bool blocked = Physics2D.Linecast(linecastPos + new Vector2(0, 0.25f), linecastPos + new Vector2(0.5f * MonoLib.sign(RB.velocity.x), 0.25f), WhatIsGround);

            Debug.DrawLine(linecastPos, linecastPos + new Vector2(0f, -0.5f));
            if (myTransform.right != new Vector3(MonoLib.sign(RB.velocity.x), 0f, 0f) && RB.velocity.magnitude != 0)
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }
            switch (currentState)
            {
                case States.Walking:
                    {
                        if (grounded && !blocked)
                        {
                            RB.velocity = RB.velocity.x >= 0 ? new Vector2(speed, RB.velocity.y) : new Vector2(-speed, RB.velocity.y);
                        }
                        else
                        {
                            RB.velocity = new Vector2(-RB.velocity.x, RB.velocity.y);
                            transform.Rotate(new Vector3(0, 180, 0));
                        }
                        if ((this.transform.position - player.transform.position).magnitude < chaseRad)
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
                        RB.velocity = new Vector2(chaseSpeed * MonoLib.sign(player.transform.position.x - this.transform.position.x), RB.velocity.y);
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
                        if (blocked && canJump)
                        {
                            RB.velocity = new Vector2(RB.velocity.x, jumpSpeed);
                            canJump = false;
                        }
                        if ((player.transform.position - this.transform.position).magnitude > chaseRad)
                        {
                            currentState = States.Walking;
                        }
                        if ((player.transform.position - this.transform.position).magnitude < YEETrad)
                        {
                            currentState = States.Attacking;
                        }
                    }
                    break;
                case States.Attacking:
                    {
                        if (checkGround() || grounded)
                        {
                            if (yeeted)
                            {
                                currentState = States.Running;
                                yeeted = false;
                            }
                            else
                            {
                                RB.AddForce((player.transform.position - this.transform.position) / (player.transform.position - this.transform.position).magnitude * YEETFORCE);
                                yeeted = true;
                            }
                        }
                    }
                    break;
                default:
                    {
                        currentState = States.NotWorking;
                    }
                    break;
            }
            cacheVel = RB.velocity;
            cachePause = false;
        }
        else
        {
            RB.velocity = new Vector2(0, 0);
            cachePause = true;
        }
    }
    public override void ActAwake()
    {
        
    }

    public bool checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(2 * width, 2 * height) * 1.1f, 0f, WhatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != gameObject)
            {
                return true;
            }
        }
        return false;
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
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / vertices;
        
        for (int i = 0; i < vertices; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + new Vector3(chaseRad * Mathf.Cos(i * deltaTheta), chaseRad * Mathf.Sin(i * deltaTheta), 0f),
                transform.position + new Vector3(chaseRad * Mathf.Cos((i + 1) * deltaTheta), chaseRad * Mathf.Sin((i + 1) * deltaTheta), 0f));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + new Vector3(YEETrad * Mathf.Cos(i * deltaTheta), YEETrad * Mathf.Sin(i * deltaTheta), 0f),
                transform.position + new Vector3(YEETrad * Mathf.Cos((i + 1) * deltaTheta), YEETrad * Mathf.Sin((i + 1) * deltaTheta), 0f));
        }
        Gizmos.color = Color.white;
    }
#endif
}