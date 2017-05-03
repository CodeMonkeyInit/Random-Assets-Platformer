using System;
using UnityEngine;

namespace Enemies
{
    public class Slime : MovingEnemy
    {
        private const float deadSlimeColiderHeight = .1f;

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
        }

        protected override void OnDeathByAttack()
        {
            if (!isDead)
            {
                BoxCollider2D boxColider = GetComponent<BoxCollider2D>();
                CircleCollider2D circleColider = GetComponent<CircleCollider2D>();
                audioSource.PlayOneShot(InteractionSound);
 
                circleColider.offset = new Vector2(circleColider.offset.x, circleColider.offset.y + .1f);
                animator.SetBool("isDead", true);
                boxColider.size = new Vector2(boxColider.size.x, deadSlimeColiderHeight);
            }
        }

    }
}