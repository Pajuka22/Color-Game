using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MEnemyHurts))]
public class MProjectile : MonoBehaviour
{
    public Vector2 direction; //stored direction from the controller
    public float bulletspeed = 1;
    private Rigidbody2D RB;
    public GameObject Creator;
    public float DestroyTime = 3;

    void Start()
    {
        transform.Translate(new Vector2(0.5f, 0.5f), Space.Self); //readjusting position due to non-center pivot point
        RB = GetComponent<Rigidbody2D>();
        direction /= direction.magnitude;
        RB.velocity = direction * bulletspeed; //sets the movement of the projectile
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), Creator.GetComponent<Collider2D>());
        Destroy(this.gameObject, DestroyTime);
    }
    private void Update()
    {
        if (GetComponent<MEnemyHurts>().hasDamaged)
        {
            Destroy(this.gameObject);
        }
        if (MenuController.IsPaused)
        {
            RB.velocity = new Vector2(0, 0);
        }
        else
        {
            RB.velocity = direction * bulletspeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!MonoLib.Has<MEnemy>(col.gameObject))
        {
            Destroy(this.gameObject);
        }
    }
}
