using System;

public class MovingEnemy : MovingGameObject
{
    protected virtual void Move()
    {
        throw new NotImplementedException();
    }

    protected override void OnCollision(UnityEngine.Collision2D collision)
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        Move();
    }
}


