using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepMover : MonoBehaviour
{
    [Header("Only Start pos and Finish Pos")]
    public Vector2[] pointMove;
    [SerializeField] private int currentIndex;
    [SerializeField] private int activePos;
    [SerializeField] private float speedMovePlatform;

    [SerializeField] private GameObject objectWantToActive;

    private bool active;

    private int multiplier;

    public void ActiveInteract()
    {
        active = true;
        objectWantToActive.GetComponent<Switch>()?.Activate(false);
        ChangeIndex();
    }

    public void DeactiveInteract()
    {
        ActiveInteract();
    }

    // Start is called before the first frame update
    void Start()
    {
        multiplier = currentIndex == (pointMove.Length - 1) ? -1 : 1;
        active = currentIndex == pointMove.Length - 1;
        objectWantToActive.GetComponent<Switch>()?.Activate(false);

        CheckingPlatform();
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
        if (active && (Vector2)transform.position == pointMove[currentIndex])
        {
            active = false;
            if (activePos == currentIndex)
            {
                objectWantToActive.GetComponent<Switch>()?.Activate(true);
                objectWantToActive.GetComponent<Lock>()?.Activate();
            }
        }
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
