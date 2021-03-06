﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonParent : MonoBehaviour
{
    public enum States { Deselected, Selected, Submitted };
    public States Current;
    [SerializeField]public Animator anim;
    public float SubmitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("State", (int)Current);
        //keep the animator up to date on what we're doing
    }
    public virtual void Select()
    {
        Current = States.Submitted;
        anim.SetInteger("State", 2);
    }
}
