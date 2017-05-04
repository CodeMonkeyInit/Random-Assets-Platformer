using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using GameObjects;

public class Coin : InteractableGameObject
{
    [SerializeField]
    private uint price;
    [SerializeField]
    private float minSound = 0.5f;
    [SerializeField]
    private float maxSound = 1f;

	private void DisableColider()
	{
		CircleCollider2D colider = gameObject.GetComponent<CircleCollider2D>();
		colider.enabled = false;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        MainCharacter character = other.GetComponentInParent<MainCharacter>();

        if (character != null)
        {
            CharacterCoinController coinController = character.GetComponentInChildren<CustomCharacterController>().coinController;
            audioSource.PlayOneShot(InteractionSound, Random.Range(minSound, maxSound));
            coinController.AddCoins(price);
            gameObject.GetComponent<Renderer>().enabled = false;
			DisableColider();
            DestroyAfterSoundFinished();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        MainCharacter character = collision.gameObject.GetComponent<MainCharacter>();

        if (character != null)
        {
            CharacterCoinController coinController = character.GetComponentInChildren<CustomCharacterController>().coinController;
            
			DisableColider();

            audioSource.PlayOneShot(InteractionSound, 1f);
            coinController.AddCoins(price);

            
            gameObject.GetComponent<Renderer>().enabled = false;

            DestroyAfterSoundFinished();
        }
    }
}
