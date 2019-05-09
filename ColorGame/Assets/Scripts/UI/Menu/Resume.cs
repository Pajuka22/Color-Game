using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : ButtonParent
{
    //public float animTime = 0.5f;
    // Start is called before the first frame update
    public override void Select()
    {
        base.Select();
        StartCoroutine(Quit(SubmitTime));
        Application.Quit();
    }
    public IEnumerator Quit(float time)
    {
        yield return new WaitForSeconds(time);
        MenuController.IsPaused = false;
    }
}
