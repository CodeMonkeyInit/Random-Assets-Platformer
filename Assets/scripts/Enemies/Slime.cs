using System;
using UnityEngine;

namespace PlatformerEnemies
{
    public class Slime : MovingEnemy
    {
        private const float deadSlimeColiderHeight = .1f;

        protected override void OnCollision(Collision2D collision)
        {
            throw new NotImplementedException();
        }

        protected override void OnDeath()
        {
            BoxCollider2D boxColider = GetComponent<BoxCollider2D>();
            CircleCollider2D circleColider = GetComponent<CircleCollider2D>();

            circleColider.enabled = false;
            animator.SetBool("isDead", true);
            boxColider.size = new Vector2(boxColider.size.x, deadSlimeColiderHeight);
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        //FIXME just for lulz, I mean test
        private void OnBecameInvisible()
        {
            if (isDead)
            {
                Destroy(this.gameObject);
            }
        }
    }
}