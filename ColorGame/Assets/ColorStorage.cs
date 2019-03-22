using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStorage : MonoBehaviour
{
    public Colors.ColorEnum Current;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IAmColor>() != null)
        {
            Current = collision.gameObject.GetComponent<IAmColor>().WhatColorAmI;
        }
    }
}
