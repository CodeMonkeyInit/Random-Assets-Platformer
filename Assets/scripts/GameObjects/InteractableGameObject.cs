using System;
using UnityEngine;

public abstract class InteractableGameObject : MonoBehaviour
{
    [SerializeField]
    protected int health;
    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected bool isDead;

    public bool IsDead { get { return isDead; } }

    protected void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    protected abstract void OnCollisionEnter2D(Collision2D collision);
}
