using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MProjCtrl : MParent
{
    public float direction = 1; //used to store player direction even when no movement is occuring
    private bool createprojectile = true;
    public float bullettime; //time between projectiles
    private Movement movement;
    public GameObject Projectile;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //finds the direction that the player faced most recently
        if (movement.speed != 0)
        {
            direction = movement.speed;
        }
        
        //determines if the projectile can be created; if so, one is created
        if (Input.GetButtonDown("Fire1") && createprojectile)
        {
            createprojectile = false; //prevents projectile spam
            GameObject newProjectile = Instantiate(Projectile) as GameObject;
            newProjectile.transform.position = new Vector2(transform.position.x, transform.position.y);
            newProjectile.GetComponent<MProjectile>().direction = direction; //tells the projectile the direction to go in
            Invoke("reload", bullettime); //sets up for projectiles to be re-enabled
        }
    }

    void reload()
    {
        createprojectile = true;
    }
}
