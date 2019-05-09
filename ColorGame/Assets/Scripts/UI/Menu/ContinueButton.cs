using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : ButtonParent
{
    // Start is called before the first frame update
    MovementController movement;
    void Start()
    {
        movement = GameObject.Find("Player").GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Select()
    {
        StartCoroutine(Continue(SubmitTime));
        movement.Respawn();
    }
    public IEnumerator Continue(float time)
    {
        yield return new WaitForSeconds(time);
        movement.Respawn();
    }
}
