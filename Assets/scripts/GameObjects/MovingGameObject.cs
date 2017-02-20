using System;
using UnityEngine;

public abstract class MovingGameObject : InteractableGameObject
{
    [SerializeField]
    protected float maxSpeed;

    protected bool IsGrounded()
    {
        throw new NotImplementedException();
    }

    protected bool IsTouchingWall()
    {
        throw new NotImplementedException();
    }

}


