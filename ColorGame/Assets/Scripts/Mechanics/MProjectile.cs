using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MProjectile : MonoBehaviour
{
    public Vector2 direction; //stored direction from the controller
    public float bulletspeed = 1;
    private Rigidbody2D RB;


    void Start()
    {
        transform.Translate(new Vector2(0.5f, 0.5f), Space.Self); //readjusting position due to non-center pivot point
        RB = GetComponent<Rigidbody2D>();
        direction /= direction.magnitude;
        RB.velocity = direction * bulletspeed; //sets the movement of the projectile
    }
}
