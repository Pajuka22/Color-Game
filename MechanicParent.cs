﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicParent : MonoBehaviour
{
    public bool WOKE = false;// { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Activate()
    {
        WOKE = true;
    }
}