using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpike : MHurts
{

    // Start is called before the first frame update
    void Start()
    {
        if (WOKE)
        {
            Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Activate()
    {
        base.Activate();
        for(int i = 0; i < GetComponents<Collider2D>().Length; i++)
        {
            GetComponents<Collider2D>()[i].isTrigger = false;
        }
    }
}
