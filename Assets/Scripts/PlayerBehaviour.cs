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
        anim.SetTrigger("Jump");

        if (rigidbody.velocity.y > 0) rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.AddForce(Vector2.up * status.JUMP_POWER);
    }

    private void FlipCharacter(bool flip)
    {
        sprite.flipX = flip;
    }
}
