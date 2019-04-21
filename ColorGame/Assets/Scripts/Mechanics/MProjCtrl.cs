using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MProjCtrl : MParent
{
    public Vector2 direction = new Vector2(1f, 0f); //used to store player direction even when no movement is occuring
    private bool createprojectile = true;
    public float bullettime; //time between projectiles
    private Movement movement;
    public GameObject Projectile;
    private Transform spawnLoc;

    void Start()
    {
        movement = GetComponent<Movement>();
        spawnLoc = transform.Find("ShootPos");
    }

    // Update is called once per frame
    void Update()
    {
        //finds the direction that the player faced most recently
        direction = this.transform.right;
        //determines if the projectile can be created; if so, one is created
        if (Input.GetButtonDown("Fire1") && createprojectile && WOKE)
        {
            createprojectile = false; //prevents projectile spam
            GameObject newProjectile = Instantiate(Projectile) as GameObject;
            for (int i = 0; i < GetComponents<Collider2D>().Length; i++)
            {
                Physics2D.IgnoreCollision(this.gameObject.GetComponents<Collider2D>()[i], newProjectile.GetComponent<Collider2D>());
            }
            newProjectile.transform.position = new Vector2(spawnLoc.transform.position.x, spawnLoc.transform.position.y);
            newProjectile.GetComponent<MProjectile>().direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; //tells the projectile the direction to go in
            Invoke("reload", bullettime); //sets up for projectiles to be re-enabled
        }
    }

    void reload()
    {
        createprojectile = true;
    }
}
