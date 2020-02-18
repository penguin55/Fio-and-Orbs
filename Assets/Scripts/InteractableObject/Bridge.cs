using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, Generator
{
    public Vector2[] pointMove;
    [SerializeField] private int currentIndex;
    [SerializeField] private float speedMovePlatform;

    [SerializeField] private bool active;

    private int multiplier;

    // To active the effect after the payer interact the generator such as active the lever
    public void ActiveInteract()
    {
        active = true;
    }

    // To deactive the effect after the payer interact the generator such as deactive the lever
    public void DeactiveInteract()
    {
        active = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        multiplier = currentIndex == (pointMove.Length - 1) ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        MovementPlatform();
        CheckingPlatform();
    }

    // Make this object move when this behaviour has true value on active variable. Active variable indicate that the level has been interact
    // and activate this object to move
    void MovementPlatform()
    {
        if (active) transform.position = Vector2.MoveTowards(transform.position, pointMove[currentIndex], Time.deltaTime * speedMovePlatform);
    }

    // To checks if the object has reached the destination pads of movement. Because, the movement is using MoveTowards, so position will get exactly in precision
    // value as specified to move into.
    void CheckingPlatform()
    {
        if (active && (Vector2)transform.position == pointMove[currentIndex]) ChangeIndex();
    }

    // To change the index when the object was arrive, so the object can move from current point to next point. 
    // Change index system ; 0 -> 1 -> ... -> n -> ... -> 1 -> 0 -> ...
    void ChangeIndex()
    {
        currentIndex += multiplier;

        if (currentIndex >= pointMove.Length)
        {
            multiplier = -1;
            currentIndex = pointMove.Length - 1;
            currentIndex += multiplier;
        }
        else if (currentIndex < 0)
        {
            multiplier = 1;
            currentIndex = 0;
            currentIndex += multiplier;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y < transform.position.y)
            {
                ChangeIndex();
            }
        }
    }
}
