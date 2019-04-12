using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MFlier : MEnemy
{
    public float shootRad = 15;
    public float moveSpeed = 5;
    public GameObject projectile;
    public float projectileSpeed = 18;
    public float rightRange = 0.5f;
    public float upRange = 0.5f;
    public LayerMask WhatIsGround;
    Random random;
    public enum States {NotWorking, Moving, Shooting}
    States currentState = States.NotWorking;
    public GameObject Target;
    public Rigidbody2D RB;
    bool tookDamage;
    GameObject newProj;
    float curHealth;
    // Start is called before the first frame update
    void Start()
    {
        random = new Random();
        Target = GameObject.Find("/Player");
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case States.NotWorking:
                {

                }
                break;
            case States.Moving:
                {
                    if(MWalker.sign(RB.velocity.x) != MWalker.sign(this.transform.right.x))
                    {
                        transform.Rotate(0, 180, 0);
                    }
                    if (checkRight())
                    {
                        RB.velocity = new Vector2(-RB.velocity.x, RB.velocity.y);
                    }
                    if (checkUp())
                    {
                        RB.velocity = new Vector2(RB.velocity.x, -RB.velocity.y);
                    }
                }
                break;
            case States.Shooting:
                {
                    
                }
                break;
        }
    }
    void Move()
    {
        currentState = States.Moving;
        float direction = Random.Range(0, 2*Mathf.PI);
        RB.velocity = new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * moveSpeed;
        StartCoroutine(WaitSec(2.5f, curHealth));
    }
    void Shoot(GameObject Target)
    {
        newProj = Instantiate(projectile) as GameObject;
        newProj.transform.position = this.transform.position;
        newProj.GetComponent<MProjectile>().direction = (Target.transform.position - this.transform.position);
        Move();
    }
    bool checkRight()
    {
        return Physics2D.Linecast(this.transform.position, this.transform.position + this.transform.right * rightRange * MWalker.sign(RB.velocity.x), WhatIsGround);
    }
    bool checkUp()
    {
        return Physics2D.Linecast(this.transform.position, this.transform.position + this.transform.up * upRange * MWalker.sign(RB.velocity.y), WhatIsGround);
    }
    public override void Activate()
    {
        Move();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject == newProj)
        {
            Physics2D.IgnoreCollision(col.collider, col.otherCollider);
        }
        if(MovementController.Has<MProjectile>(col.gameObject) && col.gameObject.tag == "ofPlayer")
        {
            curHealth -= 1;
            if(curHealth <= 0)
            {
                Destroy(this, 0);
            }
        }
    }
    IEnumerator StartShooting(float Time)
    {
        RB.velocity = Vector2.zero;

        if(MWalker.sign(Target.transform.position.x - this.transform.position.x) != this.transform.right.x)
        {
            transform.Rotate(new Vector3(0f, 180f, 0f));
        }
        currentState = States.Shooting;
        yield return new WaitForSeconds(Time);
        Shoot(Target);
    }
    IEnumerator WaitSec(float Time, float health)
    {
        yield return new WaitForSeconds(Time);
        if (curHealth < health)
        {
            yield break;
        }
        StartCoroutine(StartShooting(1f));
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position - new Vector3(0f, upRange, 0), this.transform.position + new Vector3(0f, upRange, 0));
        Gizmos.DrawLine(this.transform.position - new Vector3(rightRange, 0f, 0f), this.transform.position + new Vector3(rightRange, 0f, 0f));
    }
#endif
}
