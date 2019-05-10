using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    public GameObject Paused;
    public GameObject Dead;
    bool cachePause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if you press escape, pause the game.
        if (Input.GetKeyDown("escape") && !MenuController.IsPaused)
        {
            MenuController.IsPaused = true;
        }
        //if paused, then if it's dead open the death menu, otherwise open the pause menu, otherwise deactivate both menus.
        if (MenuController.IsPaused)
        {
            if (MenuController.IsDead)
            {
                Dead.SetActive(true);
                if (MonoLib.Has<MenuController>(Dead) && !cachePause)
                {
                    Dead.GetComponent<MenuController>().Buttons[Dead.GetComponent<MenuController>().index].Current = ButtonParent.States.Selected;
                }
            }
            else
            {
                if (MonoLib.Has<MenuController>(Paused) && !cachePause)
                {
                    Paused.GetComponent<MenuController>().Buttons[Paused.GetComponent<MenuController>().index].Current = ButtonParent.States.Selected;
                }
                Paused.SetActive(true);
            }
        }
        else
        {
            Dead.SetActive(false);
            Paused.SetActive(false);
        }
        cachePause = MenuController.IsPaused;
    }
}
