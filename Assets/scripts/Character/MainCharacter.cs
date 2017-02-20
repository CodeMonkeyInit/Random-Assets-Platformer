using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using System;
using UnityStandardAssets._2D;

namespace Character
{
    public class MainCharacter : MovingGameObject
    {
        [SerializeField]
        private LayerMask whatIsGround;
        //check
        [SerializeField]
        private float jumpForce = 10f;
        [SerializeField]
        private float slideForce = 1f;
        [SerializeField]
        private bool airControl;

        const float groundedRadius = 0.2f;


        private bool isGrounded;
        private bool facingForward = true;
        private bool slideDone;



        // Use this for initialization
        private void Awake()
        {

            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        protected override void  OnCollision(Collision2D collision)
        {
            throw new NotImplementedException();
        }

        void OnCollisionStay2D(Collision2D collider)
        {
            CheckIfGrounded();
        }

        void OnCollisionExit2D(Collision2D collider)
        {
            isGrounded = false;
        }

        private void CheckIfGrounded()
        {
            RaycastHit2D[] hits;

            //We raycast down 1 pixel from this position to check for a collider
            Vector2 positionToCheck = transform.position;
            hits = Physics2D.RaycastAll(positionToCheck, new Vector2(0, -1), 0.01f);

            //if a collider was hit, we are grounded
            if (hits.Length > 0)
            {
                isGrounded = true;
            }
        }
	
        // Update is called once per frame
        private void FixedUpdate()
        {
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("VerticalSpeed", rigidBody.velocity.y);
        }

        private void Flip()
        {
            Vector3 theScale;
            facingForward = !facingForward;
            theScale = transform.localScale;
            theScale.x *= -1;

            transform.localScale = theScale;
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

                if (move > 0 && false == facingForward)
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