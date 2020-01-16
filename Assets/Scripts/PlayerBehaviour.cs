using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerInfo status;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Animator anim;

    private bool falling;
    private bool onJump;
    private bool onGround;

    public void Initialize()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Walk(Vector2 direction)
    {
        if (direction.x > 0) FlipCharacter(false);
        if (direction.x < 0) FlipCharacter(true);

        transform.Translate(direction * (status.MOVING_SPEED * Time.deltaTime));

        anim.SetBool("Run", direction != Vector2.zero);
    }

    public void Jump()
    {
        if (onGround)
        {
            onJump = true;
            onGround = false;
            anim.SetTrigger("Jump");

            if (rigidbody.velocity.y > 0) rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(Vector2.up * status.JUMP_POWER);
        }
    }

    public void Fall()
    {
        if (rigidbody.velocity.y < 0)
        {
            if (!falling)
            {
                onGround = false;
                onJump = true;
                falling = true;
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Fall");
            }
        }
    }

    public void Land()
    {
        if ((falling || onJump) && onGround)
        {
            onJump = false;
            falling = false;
            anim.ResetTrigger("Fall");
            anim.SetTrigger("Land");
        }
    }

    private void FlipCharacter(bool flip)
    {
        sprite.flipX = flip;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (falling)
            {
                onGround = true;
                falling = false;
                onJump = true;
                anim.ResetTrigger("Fall");
                anim.SetTrigger("Land");
            }
        }
    }
}
