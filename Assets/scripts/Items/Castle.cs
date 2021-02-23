using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public GameObject fireworks;

    private bool fireworksTriggered = false;

    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (fireworksTriggered)
            return;

        fireworksTriggered = true;

        Instantiate(fireworks);
    }
}