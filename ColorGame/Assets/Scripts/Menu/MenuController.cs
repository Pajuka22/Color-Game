using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] List<ButtonParent> Buttons;
    bool keydown = false;
    public int index = 0;
    public static bool IsPaused = false;
    bool submitted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!IsPaused)
        {
            if (!submitted)
            {
                if (Input.GetAxis("Vertical") != 0)
                {
                    if (!keydown)
                    {

                        if (Input.GetAxis("Vertical") < 0)
                        {
                            index++;
                            Buttons[index - 1].Current = ButtonParent.States.Deselected;
                        }
                        else
                        {
                            index--;
                            Buttons[index+1].Current = ButtonParent.States.Deselected;
                            if (index < 0)
                            {
                                index = Buttons.Count - 1;
                            }
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
            }
            if (Input.GetButtonDown("Submit"))
            {
                Buttons[index].Current = ButtonParent.States.Submitted;
                //Invoke("Submit", Buttons[index].SubmitTime);
                //submitted = true;
            }
        }
    }
    void Submit()
    {
        Buttons[index].Select();
    }
}