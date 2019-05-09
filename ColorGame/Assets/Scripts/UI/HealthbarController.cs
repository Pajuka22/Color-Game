using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour
{
    public GameObject Bar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ColorStorage.Current[2] && !MenuController.IsPaused)
        {
            if (!Bar.activeSelf)
            {
                Bar.SetActive(true);
            }
        }
        else
        {
            if (Bar.activeSelf)
            {
                Bar.SetActive(false);
            }
        }
    }
}
