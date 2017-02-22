using UnityEngine;
using System;
using UnityStandardAssets._2D;
using PlatformerEnemies;

namespace Character
{
    public class MainCharacter : MovingGameObject
    {
        //check
        [SerializeField]
        private float jumpForce = 10f;
        [SerializeField]
        private float slideForce = 1f;
        [SerializeField]
        private bool airControl;
        [SerializeField]
        private readonly ushort jumpsAllowed = 2;

        private bool isGrounded;
        private bool slideDone;

        private ushort jumpsCount;

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 enemyAttackVector = new Vector2(-1f, 0f);
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();

            if (enemy != null)
            {
                foreach (ContactPoint2D contactPoint in collision.contacts)
                {
                    Debug.Log(contactPoint.normal);
                    Debug.DrawLine(contactPoint.point, contactPoint.point + contactPoint.normal, Color.red, 10);
                    if (contactPoint.normal != enemyAttackVector)
                    {
                        enemy.Hurt();
                    }
                    else
                    {
                        OnDeath();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            isGrounded = IsGrounded();
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("VerticalSpeed", rigidBody.velocity.y);
        }

        public void Move(float move, CharacterStatus status)
        {
            if (!isDead)
            {
                bool isCrouched;
                if (status == CharacterStatus.Crouched)
                {
                    isCrouched = true;
                }
                else
                {
                    isCrouched = false;
                }

                animator.SetBool("isCrouched", isCrouched);

                Debug.Log(jumpsCount);

                if (isGrounded || (airControl && !IsTouchingWall()))
                {
                    if (isGrounded)
                    {
                        jumpsCount = jumpsAllowed;
                    }
                    if (!isCrouched)
                    {
                        rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);
                        slideDone = false;
                    }
                    else if (!slideDone)
                    {
                        rigidBody.AddForce(new Vector2(move * slideForce, rigidBody.velocity.y));
                    }
                    animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

                    if (move > 0 && !facingForward)
                    {
                        ChangeDirection();
                    }
                    else if (move < 0 && facingForward)
                    {
                        ChangeDirection();	
                    }

                    if (status == CharacterStatus.Jumping && jumpsCount != 0)
                    {
                        jumpsCount--;
                        animator.SetBool("isGrounded", isGrounded);
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                    }
                }
            }
        }
    }
}