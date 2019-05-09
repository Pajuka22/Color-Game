using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    public GameObject Paused;
    public GameObject Dead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && !MenuController.IsPaused)
        {
            MenuController.IsPaused = true;
        }
        if (MenuController.IsPaused)
        {
            if (MenuController.IsDead)
            {
                Dead.SetActive(true);
            }
            else
            {
                Paused.SetActive(true);
            }
        }
        else
        {
            Dead.SetActive(false);
            Paused.SetActive(false);
        }
    }
}
