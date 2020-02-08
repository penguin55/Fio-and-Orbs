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
    [HideInInspector] public bool idle;

    private Interactable interactableObject;

    public void Initialize()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        idle = true;
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
        if (onGround && !onJump)
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
        if (rigidbody.velocity.y < -0.2f)
        {
            if (!falling)
            {
                onGround = false;
                onJump = true;
                falling = true;
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Fall");
                idle = false;
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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
            if (collision.gameObject.CompareTag("Platform"))
            {
                transform.parent = collision.transform;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            if (falling)
            {
                onGround = true;
                falling = false;
                onJump = false;
                anim.ResetTrigger("Fall");
                anim.SetTrigger("Land");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }

    public bool IsPlayerIdle()
    {
        return idle;
    }

    public void Interactable(Interactable interacts)
    {
        interactableObject = interacts;
    }

    public Interactable GetInteractableObject()
    {
        return interactableObject;
    }
}
