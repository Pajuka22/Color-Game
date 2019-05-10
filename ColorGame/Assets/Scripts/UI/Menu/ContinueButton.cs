using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : ButtonParent
{
    // Start is called before the first frame update
    MovementController movement;
    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GameObject.Find("Player").GetComponent<MovementController>();
    }
    public override void Select()
    {
        base.Select();
        StartCoroutine(Continue(SubmitTime));
    }
    public IEnumerator Continue(float time)
    {
        yield return new WaitForSeconds(time);
        movement.Respawn();
        MenuController.IsPaused = false;
        MenuController.IsDead = false;
        //respawn after time.
    }
}
