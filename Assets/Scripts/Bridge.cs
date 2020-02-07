using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, Generator
{
    public Vector2[] pointMove;
    [SerializeField] private int currentIndex;
    [SerializeField] private float speedMovePlatform;

    private bool active;

    private int multiplier;

    public void ActiveInteract()
    {
        active = true;
    }

    public void DeactiveInteract()
    {
        active = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        multiplier = multiplier == (pointMove.Length - 1) ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        MovementPlatform();
        CheckingPlatform();
    }

    void MovementPlatform()
    {
        if (active) transform.position = Vector2.MoveTowards(transform.position, pointMove[currentIndex], Time.deltaTime * speedMovePlatform);
    }

    void CheckingPlatform()
    {
        if (active && (Vector2)transform.position == pointMove[currentIndex]) ChangeIndex();
    }

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
}
