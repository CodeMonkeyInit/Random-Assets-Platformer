using UnityEngine;
using System;
using Enemies;
using GameObjects;

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

		public float MaxSpeed
		{
			get{ return maxSpeed; }
		}

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
			const float accuracy = 0.1f;

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
					} else if (!(enemy as InteractableGameObject).IsDead)
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
			/*DO NOTHING*/
		}
			
		public void AddLives(int lives)
		{
			health += lives;
		}

		public void Move(float move, CharacterStatus status)
		{
			if (!isDead)
			{
				bool isCrouched = status == CharacterStatus.Crouched;

				animator.SetBool("isCrouched", isCrouched);

				if (isGrounded || (airControl && !IsTouchingWall()))
				{

					if (!isCrouched)
					{
						float speed = maxSpeed * move;

						if (Mathf.Abs(speed) > maxSpeed)
						{
							speed = speed < 0 ? -maxSpeed : maxSpeed;
						}

						rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
					} else
					{
						rigidBody.AddForce(new Vector2(move * slideForce, rigidBody.velocity.y));
					}
					animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

					if (move > 0 && !facingForward)
					{
						ChangeDirection();
					} else if (move < 0 && facingForward)
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

		public void Hurt(bool deathCausedByEnemy = true)
		{
			health--;
			if (deathCausedByEnemy)
			{
				OnDeathByAttack();
			} else
			{
				OnDeath();
			}
			DestroyAfterSoundFinished();
		}
	}
}