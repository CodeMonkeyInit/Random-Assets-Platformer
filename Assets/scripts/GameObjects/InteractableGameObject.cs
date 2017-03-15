using System;
using UnityEngine;
using System.Collections;

public abstract class InteractableGameObject : BasicGameObject
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

    public int Health
    {
        get { return health; }
    }

    public bool IsDead { get { return isDead; } }

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    protected void DestroyAfterSoundFinished()
    {
        Destroy(this.gameObject, interactionSoundLength);
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);
}