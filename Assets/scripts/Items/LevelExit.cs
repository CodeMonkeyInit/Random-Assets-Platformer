using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using GameObjects;

public class LevelExit : BasicGameObject
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        CustomCharacterController characterController = other.GetComponentInChildren<CustomCharacterController>();

        if (characterController != null)
        {
            characterController.SetLevelCompleted();
        }
    }
}
