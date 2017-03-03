using UnityEngine;
using System;
using UnityStandardAssets._2D;
using PlatformerEnemies;

namespace Character
{
    public class MainCharacter : MovingGameObject
    {
        [SerializeField]
        private AudioClip[] jumpSounds;
        [SerializeField]
        private float jumpForce = 10f;
        [SerializeField]
        private float slideForce = 1f;
        [SerializeField]
        private bool airControl;
        [SerializeField]
        private short jumpsAllowed = 1;

        private AudioClip JumpSound
        {
            get { return jumpSounds.GetRandomSound(); }
        }

        private bool isGrounded;
        private short jumpsCount;

        private void CheckIfPlayerHangingByTheCliff(Collision2D collision)
        {
            Vector2 aboveWallCollisionVector = new Vector2(0f, -1f);

            if (!isGrounded && !IsTouchingWall())
            {
                if (collision.gameObject.GetComponent<PolygonCollider2D>() != null)
                {
                    jumpsCount = jumpsAllowed;
                    foreach (ContactPoint2D contactPoint in collision.contacts)
                    {
                        if (contactPoint.normal == aboveWallCollisionVector)
                        {
                            jumpsCount = 0;
                            break;
                        }
                    }
                }
            }
        }

        private void CheckIfPlayerColidingWithEnemy(Collision2D collision)
        {
            Vector2 enemyAttackVector = new Vector2(1f, 0f);
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
            const float accuracy = 0.2f;

            if (enemy != null)
            {
                foreach (ContactPoint2D contactPoint in collision.contacts)
                {
                    Debug.Log(contactPoint.normal);
                    Debug.DrawLine(contactPoint.point, contactPoint.point + contactPoint.normal, Color.red, 10);
                    //TODO Find optimal vector
                    if (Math.Abs((contactPoint.normal.Abs().x - enemyAttackVector.x)) > accuracy)
                    {
                        enemy.Hurt();
                    }
                    else if (!(enemy as InteractableGameObject).IsDead)
                    {
                        Hurt();
                        break;
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

        private void Jump()
        {
            if (isGrounded)
            {
                jumpsCount = jumpsAllowed;
            }
            jumpsCount--;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            audioSource.PlayOneShot(JumpSound);
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            CheckIfPlayerHangingByTheCliff(collision);
            CheckIfPlayerColidingWithEnemy(collision);
        }

        protected override void OnBecameInvisible()
        {
            //DO NOTHING
        }

        public void AddLives(int lives)
        {
            health += lives;
        }

        public void Move(float move, CharacterStatus status)
        {
            if (!isDead)
            {
                bool isCrouched = false;

                if (status == CharacterStatus.Crouched)
                {
                    isCrouched = true;
                }

                animator.SetBool("isCrouched", isCrouched);

                if (isGrounded || (airControl && !IsTouchingWall()))
                {

                    if (!isCrouched)
                    {
                        rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);
                    }
                    else
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

                    if (status == CharacterStatus.Jumping && (jumpsCount > 0 || isGrounded))
                    {
                        Jump();
                    }
                }
            }
        }

        public void Hurt()
        {
            //FIXME
            GameObject.FindObjectOfType<Camera2DFollow>().target = null;
            //FIXME

            OnDeath();
            StartCoroutine(DestroyAfterSoundFinished());
        }
    }
}