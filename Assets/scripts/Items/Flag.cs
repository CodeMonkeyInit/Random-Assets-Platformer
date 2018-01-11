using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using System;
using Checkpoint;
using GameObjects;

public class Flag : InteractableGameObject
{
    private GameObjectSpawner spawner;

    private void SetCheckPoint()
    {
        CheckpointController.SetCheckpoint(spawner.ID);
    }
        
    protected override void Awake()
    {
        base.Awake();
        isDead = true; 
        spawner = GetComponentInChildren<GameObjectSpawner>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            MainCharacter character = collision.gameObject.GetComponent<MainCharacter>();

            if (character != null)
            {
                BoxCollider2D colider = gameObject.GetComponent<BoxCollider2D>();
                isDead = false;

                animator.SetBool("activated", true);

                audioSource.PlayOneShot(InteractionSound, 1f);
                colider.enabled = false;

                SetCheckPoint();
            }
        }
    }




}
