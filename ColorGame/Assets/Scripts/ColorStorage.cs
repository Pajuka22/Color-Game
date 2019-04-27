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
        if (MonoLib.Has<IAmColor>(collision.gameObject) && Eff != null)
        {
            switch (collision.gameObject.GetComponent<IAmColor>().WhatColorAmI)
            {
                case Colors.ColorEnum.Red:
                    Eff.IncreaseColor(ref Eff.IRed, 1);
                    break;
                case Colors.ColorEnum.Orange:
                    Eff.IncreaseColor(ref Eff.IOrange, 1);
                    break;
                 case Colors.ColorEnum.Yellow:
                    Eff.IncreaseColor(ref Eff.IYellow, 1);
                    break;
                case Colors.ColorEnum.Green:
                    Eff.IncreaseColor(ref Eff.IGreen, 1);
                    break;
                case Colors.ColorEnum.Blue:
                    Eff.IncreaseColor(ref Eff.IBlue, 1);
                    break;
                case Colors.ColorEnum.Purple:
                    Eff.IncreaseColor(ref Eff.IPurple, 1);
                    break;
            }
            Current[(int)collision.gameObject.GetComponent<IAmColor>().WhatColorAmI] = true;
        }
    }
}
