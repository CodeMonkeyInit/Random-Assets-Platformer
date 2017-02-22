using System;
using UnityEngine;

public abstract class MovingGameObject : InteractableGameObject
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    protected LayerMask whatIsGround;
    [SerializeField]
    protected LayerMask whatIsWall;
    [SerializeField]
    protected float maxSpeed;
    [SerializeField]
    protected bool facingForward;
    [SerializeField]
    protected float heightMiddle = .05f;
    [SerializeField]
    protected float widthMiddle = 1f;
    [SerializeField]
    protected float groundCheckLineLength = 0.7f;
    protected readonly Vector2 deathVelocity = new Vector2(0, 7f);

    private Vector3 TransformAxis
    {
        get
        {
            if (facingForward)
            {
                return transform.right;
            }
            else
            {
                return -transform.right;
            }
        }
    }

    private SpriteRenderer GetSpriteRenderer
    {
        get
        {
            if (null == spriteRenderer)
            {
                spriteRenderer = this.GetComponent<SpriteRenderer>();
            }

            return spriteRenderer;
        }
    }

    private float SpriteWidth
    { 
        get { return GetSpriteRenderer.bounds.extents.x; }
    }

    private float SpriteHeight
    {
        get { return GetSpriteRenderer.bounds.extents.y; }
    }

    protected void Flip()
    {
        Vector3 tempScale;
        facingForward = !facingForward;
        tempScale = transform.localScale;
        //Reverse Character position
        tempScale.x *= -1;

        transform.localScale = tempScale;
    }

    protected bool IsGrounded()
    {
        bool isGrounded;

        Vector2 lineCastPosition = transform.position - TransformAxis * SpriteWidth * widthMiddle;
        Debug.DrawLine(lineCastPosition, lineCastPosition + new Vector2(0, -groundCheckLineLength));

        isGrounded = Physics2D.Linecast(lineCastPosition, lineCastPosition + new Vector2(0, -groundCheckLineLength), whatIsGround);

        return isGrounded;
    }

    protected bool IsTouchingWall()
    {
        bool isTouchingWall;


        Vector2 lineCastPosition = transform.position - TransformAxis * SpriteWidth;
        Debug.DrawLine(lineCastPosition, lineCastPosition - TransformAxis.toVector2() * heightMiddle);

        isTouchingWall = Physics2D.Linecast(lineCastPosition, 
            lineCastPosition - transform.right.toVector2() * heightMiddle, 
            whatIsWall);

        return isTouchingWall;
    }

    protected virtual void OnDeath()
    {
        foreach (Collider2D colider in GetComponents<Collider2D>())
        {
            animator.SetBool("isDead", true);
            colider.enabled = false;
        }

        rigidBody.velocity = deathVelocity;
    }

}


