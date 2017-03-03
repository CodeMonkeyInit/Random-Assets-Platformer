using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class LevelEnd : InteractableGameObject
{
    protected override void  OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        MainCharacter character = collider.GetComponentInParent<MainCharacter>();

        Debug.Log("Trigger");
        if (character != null)
        {
            character.Hurt();
        }
    }
}
