using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerInfo status;
    [SerializeField] private OrbBehaviour orbs;

    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Animator anim;

    private bool falling;
    private bool onJump;
    private bool onGround;
    protected bool knockback;
    private bool die;
    [HideInInspector] public bool idle;

    private Interactable interactableObject;

    public void Initialize()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        idle = true;
        UIManager.instance.SetHealth(status.LIVES);
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
            idle = false;
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

    public IEnumerator Knockback(Transform enemy)
    {
        if (!knockback)
        {
            idle = false;
            rigidbody.velocity = Vector2.zero;
            knockback = true;

            anim.SetBool("Knockback",knockback);

            if (enemy.position.x > transform.position.x)
            {
                rigidbody.AddForce(new Vector2(-1, 1) * 3, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody.AddForce(new Vector2(1, 1) * 3, ForceMode2D.Impulse);
            }

            TakeDamage();

            yield return new WaitForSeconds(1);
            knockback = false;
            anim.SetBool("Knockback", knockback);
        }
        yield return 0;
    }

    private void TakeDamage()
    {
        if (die) return;
        status.LIVES--;
        UIManager.instance.SetHealth(status.LIVES);
        
        if (status.LIVES == 0 && !die)
        {
            Die();
        }
    }
    
    private void TakeDamage(int damage)
    {
        if (die) return;
        status.LIVES -= damage;
        UIManager.instance.SetHealth(status.LIVES);
        
        if (status.LIVES == 0 && !die)
        {
            Die();
        }
    }

    private void Die()
    {
        GameVariables.FreezeGame = true;
        UIManager.instance.SendMessageGameOver("Lose", true);
        GetComponent<Collider2D>().enabled = false;
        die = true;
        anim.SetBool("Run",false);
        anim.SetTrigger("Die");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
            if (collision.gameObject.CompareTag("Platform"))
            {
                if (transform.position.y > collision.transform.position.y)
                {
                    transform.parent = collision.transform;
                    orbs.SetOnPlatform(true);
                }
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
            if (transform.parent != null)
            {
                transform.parent = null;
                orbs.SetOnPlatform(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            TakeDamage(4);
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
