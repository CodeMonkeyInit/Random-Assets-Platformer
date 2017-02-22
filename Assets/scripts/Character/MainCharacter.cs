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

        private bool isGrounded;
        private bool slideDone;

        // Use this for initialization
        private void Awake()
        {
            facingForward = true;
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        protected override void  OnCollision(Collision2D collision)
        {
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
            if (enemy != null)
            {
                foreach (ContactPoint2D contactPoint in collision.contacts)
                {
                    Debug.Log(contactPoint.normal);
                    Debug.DrawLine(contactPoint.point, contactPoint.point + contactPoint.normal, Color.red, 10);
                    if (contactPoint.normal != new Vector2(-1f, 0f))
                    {
                        enemy.Hurt();
                    }
                    else
                    {
                        OnDeath();
                        isDead = true;
                    }
                }
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision(collision);
        }
            
        // Update is called once per frame
        private void FixedUpdate()
        {
            isGrounded = IsGrounded();
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("VerticalSpeed", rigidBody.velocity.y);
        }

        public void Move(float move, CharacterStatus status)
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

            if (isGrounded || airControl)
            {
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
                    Flip();
                }
                else if (move < 0 && facingForward)
                {
                    Flip();	
                }

                if (status == CharacterStatus.Jumping && isGrounded)
                {
                    animator.SetBool("isGrounded", isGrounded);
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                }
            }
        }
    }
}