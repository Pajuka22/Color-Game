using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] public List<ButtonParent> Buttons;
    bool keydown = false;
    public int index = 0;
    public static bool IsPaused = false;
    //bool submitted = false;
    public static bool IsDead = false;
    // Start is called before the first frame update
    void Start()
    {
        Buttons[index].Current = ButtonParent.States.Selected;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsPaused)
        {
                if (Input.GetAxis("Vertical") != 0)
                {
                    if (!keydown)
                    {//make sure that the player released it to avoid spam and ugliness.

                        if (Input.GetAxis("Vertical") > 0)
                        {
                            index++;
                            Buttons[index - 1].Current = ButtonParent.States.Deselected;
                        //increase index
                        }
                        else
                        {
                            index--;
                            Buttons[index+1].Current = ButtonParent.States.Deselected;
                            if (index < 0)
                            {
                                index = Buttons.Count - 1;
                            }
                            //decrease index
                        }
                        index = index % Buttons.Count;
                        Buttons[index].Current = ButtonParent.States.Selected;
                        keydown = true;
                    }
                }
                else
                {
                    keydown = false;
                }
            if (Input.GetButtonDown("Submit"))
            {
                Buttons[index].Current = ButtonParent.States.Submitted;
                Submit();
                
            }
        }
    }
    void Submit()
    {
        Buttons[index].Current = ButtonParent.States.Submitted;
        Buttons[index].Select();
    }
}