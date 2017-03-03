using System;
using UnityEngine;

public abstract class MovingGameObject : InteractableGameObject
{
    private SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigidBody;

    [SerializeField]
    protected LayerMask whatIsGround;
    [SerializeField]
    protected LayerMask whatIsWall;
    [SerializeField]
    protected float maxSpeed;
    [SerializeField]
    protected bool facingForward;
    [SerializeField]
    protected float wallCheckLinePosition = 1;
    [SerializeField]
    protected float wallCheckLineLength = .01f;
    [SerializeField]
    protected float groundCheckPosition = 1f;
    [SerializeField]
    protected float groundCheckMiddlePosition = 1;
    [SerializeField]
    protected float groundCheckLineLength = 0.7f;
    protected readonly Vector2 deathVelocity = new Vector2(0, 7f);

    private Vector3 TransformAxis
    {
        get
        {
            if (facingForward)
            {
                return -transform.right;
            }
            else
            {
                return transform.right;
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

    private bool IsGroundedMiddle()
    {
        bool isGroundedMiddle;
        Vector2 lineCastMiddlePosition = transform.position - TransformAxis * SpriteWidth * groundCheckMiddlePosition;

        Debug.DrawLine(lineCastMiddlePosition, lineCastMiddlePosition + new Vector2(0, -groundCheckLineLength));
        isGroundedMiddle = Physics2D.Linecast(lineCastMiddlePosition, lineCastMiddlePosition + new Vector2(0, -groundCheckLineLength), whatIsGround);

        return isGroundedMiddle;
    }

    protected override void Awake()
    {
        base.Awake();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    protected int Direction
    { 
        get
        {
            if (facingForward)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }

    protected virtual void OnBecameInvisible()
    {
        if (isDead)
        {
            Destroy(this.gameObject);
        }
    }

    protected void ChangeDirection()
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
        bool isGroundedMiddle = false;
        Vector2 lineCastPosition = transform.position - TransformAxis * SpriteWidth * groundCheckPosition;

        if (groundCheckMiddlePosition != 1)
        {
            isGroundedMiddle = IsGroundedMiddle();
        }
        Debug.DrawLine(lineCastPosition, lineCastPosition + new Vector2(0, -groundCheckLineLength));

        isGrounded = Physics2D.Linecast(lineCastPosition, lineCastPosition + new Vector2(0, -groundCheckLineLength), whatIsGround);

        return isGrounded || isGroundedMiddle;
    }

    protected bool IsTouchingWall()
    {
        bool isTouchingWall;

        Vector2 lineCastPosition = transform.position - TransformAxis * SpriteWidth - transform.up * SpriteHeight * wallCheckLinePosition;
        Debug.DrawLine(lineCastPosition, lineCastPosition - TransformAxis.toVector2() * wallCheckLineLength);

        isTouchingWall = Physics2D.Linecast(lineCastPosition, 
            lineCastPosition - transform.right.toVector2() * wallCheckLineLength, 
            whatIsWall);

        return isTouchingWall;
    }

    protected virtual void OnDeath()
    {
        foreach (Collider2D colider in GetComponents<Collider2D>())
        {
            colider.enabled = false;
        }
        isDead = true;
        animator.SetBool("isDead", true);
        rigidBody.velocity = deathVelocity;
        GameObject.FindObjectOfType<AudioSource>().PlayOneShot(InteractionSound);
    }
}


