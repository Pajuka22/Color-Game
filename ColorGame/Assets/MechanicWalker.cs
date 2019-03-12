using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicWalker : MechanicEnemy
{
    public enum States { NotWorking, Walking, Running, Transforming, Attacking}
    public States currentState = States.NotWorking;
    public Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState){
            case States.NotWorking:

                break;
            case States.Walking:
                if(RB.velocity.x == 0)
                {
                    Move(8, new Vector2(1, 0));
                }else if (RB.velocity.x > 0)
                {
                    
                }
                break;
        }
    }
    public override void ActAwake()
    {

    }
    public void GoApeShit()
    {

    }
    public void Move(float Speed, Vector2 Direction)
    {

    }
}
