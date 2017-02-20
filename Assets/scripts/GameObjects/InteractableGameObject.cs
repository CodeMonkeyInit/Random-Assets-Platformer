using System;
using UnityEngine;

public abstract class InteractableGameObject : MonoBehaviour
{
    protected int health;
    protected Rigidbody2D rigidBody;
    protected Animator animator;

    protected abstract void OnCollision(Collision2D collision);
}
