using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using GameObjects;

public class LevelBound : BasicGameObject
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        MainCharacter character = collider.GetComponentInParent<MainCharacter>();

        if (character != null)
        {
            character.Hurt(false);
        }
    }
}