using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public enum ColorEnum {White, Black, Red, Orange, Yellow, Green, Blue, Purple, Grey};
    public ColorEnum WhatColor = ColorEnum.White;
    public MParent Mechanic;
    ColorStorage Collected;
    // Start is called before the first frame update
    void Start()
    {
        Collected = GameObject.Find("Player").GetComponent<ColorStorage>();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WhatColor <= Collected.Current)
        {
            Mechanic.Activate();
            enabled = false;
        }
    }
}
