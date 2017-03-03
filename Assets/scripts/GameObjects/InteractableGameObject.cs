using System;
using UnityEngine;
using System.Collections;

public abstract class InteractableGameObject : MonoBehaviour
{
    [SerializeField]
    protected AudioClip[] interactionSounds;
    [SerializeField]
    private int interactionSoundLength = 1;
    [SerializeField]
    protected int health;
    protected Animator animator;
    protected bool isDead;
    protected AudioSource audioSource;

    protected AudioClip InteractionSound
    {
        get
        {
            return interactionSounds.GetRandomSound();
        }
    }

    public bool IsDead { get { return isDead; } }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    protected IEnumerator DestroyAfterSoundFinished()
    {
        yield return new WaitForSeconds(interactionSoundLength);

        Destroy(this.gameObject);
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);
}