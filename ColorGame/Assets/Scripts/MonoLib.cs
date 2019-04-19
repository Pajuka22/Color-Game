using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoLib : MonoBehaviour
{
    public static bool Has<T>(GameObject GO)
    {
        return GO.GetComponent<T>() != null;
    }
    public static int sign(float val)
    {
        if (val == 0)
        {
            return 0;
        }
        else if (val < 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
