using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorStorage : MonoBehaviour
{
    public bool[] Current;
    public SelectGrayEff Eff;
    // Start is called before the first frame update
    void Start()
    {
        Current = new bool[9];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MonoLib.Has<IAmColor>(collision.gameObject))// && Eff != null)
        {
            if(Eff != null)
            {
                Eff.IncreaseColor((int)collision.gameObject.GetComponent<IAmColor>().WhatColorAmI, 1);
            }
            Current[(int)collision.gameObject.GetComponent<IAmColor>().WhatColorAmI] = true;
        }
    }
}
