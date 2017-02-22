using System;
using UnityEngine;

public abstract class InteractableGameObject : MonoBehaviour
{
    [SerializeField]
    protected int health;
    protected Rigidbody2D rigidBody;
    protected Animator animator;
    protected bool isDead;

    protected abstract void OnCollision(Collision2D collision);
}
