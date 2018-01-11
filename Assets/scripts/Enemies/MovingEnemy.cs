using System;
using UnityEngine;
using GameObjects;

namespace Enemies
{
    public class MovingEnemy : MovingGameObject, IEnemy
    {
        protected virtual void Move()
        {
            if (!isDead)
            {
                if (!IsGrounded() || IsTouchingWall())
                {
                    ChangeDirection();
                }
                rigidBody.velocity = new Vector2(maxSpeed * Direction, rigidBody.velocity.y);

                animator.SetFloat("speed", Math.Abs(rigidBody.velocity.x));
            }
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isDead)
            {
                IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();

                if (enemy != null)
                {
                    ChangeDirection();
                }
            }
        }

        protected void FixedUpdate()
        {
            Move();
        }

        public void Hurt()
        {
            OnDeathByAttack();
            isDead = true;
        }
    }
}

