﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class LevelSurface : InteractableGameObject
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        MainCharacter character = collision.gameObject.GetComponent<MainCharacter>();

        if (character != null)
        {
            if (audioSource == null)
            {
                Debug.LogError("AAAAAAAA");
            }
            audioSource.PlayOneShot(InteractionSound, 1f);
        }
    }
}