using System;
using UnityEngine;

namespace PlatformerEnemies
{
    public class MovingEnemy : MovingGameObject, IEnemy
    {
        protected int directon = 1;

        protected virtual void Move()
        {
            if (!isDead)
            {
                if (!IsGrounded() || IsTouchingWall())
                {
                    Flip();
                    directon = -directon;
                }
                rigidBody.velocity = new Vector2(maxSpeed * directon, rigidBody.velocity.y);

                animator.SetFloat("speed", Math.Abs(rigidBody.velocity.x));
            }
        }

        protected override void OnCollision(UnityEngine.Collision2D collision)
        {
            throw new NotImplementedException();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnBecomeInvisible()
        {
            if (isDead)
            {
                Destroy(this.gameObject);
            }
        }

        public void Hurt()
        {
            //TODO Destroy
            OnDeath();
            isDead = true;
        }
    }
}

