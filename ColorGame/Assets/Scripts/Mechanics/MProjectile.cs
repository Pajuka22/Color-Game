using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MProjectile : MonoBehaviour
{
    public float direction; //stored direction from the controller
    public float bulletspeed = 1;
    private Rigidbody2D RB;


    void Start()
    {
        transform.Translate(new Vector2(0.5f, 0.5f), Space.Self); //readjusting position due to non-center pivot point
        RB = GetComponent<Rigidbody2D>();
        Vector2 bd = new Vector2(direction, 0); //creates a vector based on the direction from its creation
        RB.velocity = bd * bulletspeed; //sets the movement of the projectile
    }
}
