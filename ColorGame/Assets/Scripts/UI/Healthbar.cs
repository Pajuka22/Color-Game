using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    Vector3 initScale;
    public MovementController player;
    // Start is called before the first frame update
    void Start()
    {
        initScale = GetComponent<RectTransform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (MonoLib.Has<RectTransform>(gameObject))
        {
            GetComponent<RectTransform>().localScale = new Vector3(initScale.x * player.currentHealth / player.health, initScale.y * 1, initScale.z * 1);
        }
    }
}
