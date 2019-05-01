using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEnemy : MHurts
{
    public float MaxHealth;
    float CurrentHealth;
    public LayerMask whatHurts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void ActAwake()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (WOKE)
        {
            if (MonoLib.Has<MEnemyHurts>(col.gameObject))
            {
                CurrentHealth -= col.gameObject.GetComponent<MEnemyHurts>().WOKE ? col.gameObject.GetComponent<MEnemyHurts>().Damage : 0;
                col.gameObject.GetComponent<MEnemyHurts>().hasDamaged = true;
                if (CurrentHealth <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (WOKE)
        {
            if (MonoLib.Has<MEnemyHurts>(col.gameObject))
            {
                CurrentHealth -= col.gameObject.GetComponent<MEnemyHurts>().WOKE ? col.gameObject.GetComponent<MEnemyHurts>().Damage : 0;
                col.gameObject.GetComponent<MEnemyHurts>().hasDamaged = true;
                if (CurrentHealth <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
