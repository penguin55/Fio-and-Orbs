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

    // To activate the step mover that will be activate the movement of the switch button or lever to the position that we specify
    public void ActiveInteract()
    {
        active = true;
        objectWantToActive.GetComponent<Switch>()?.Activate(false);
        objectWantToActive.GetComponent<Lever>()?.Activate(false);
        ChangeIndex();
    }

    // To toggle activate the step mover that will be activate movement the switch button or lever to the position that we specify
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


    // To move this object to the specified position according current index.
    void MovementPlatform()
    {
        if (active) transform.position = Vector2.MoveTowards(transform.position, pointMove[currentIndex], Time.deltaTime * speedMovePlatform);
    }

    // To check if the object was arrive or not, when the object is arrive and the object is on position that we specify the active condition
    // then the object will activate the generator from button switch, lever or lock that assigned to the objectWantToActive variable
    // Active condition is a position of object that we specify from the inspector to activate the object we assigns to objectWantToActive variable
    void CheckingPlatform()
    {
        if (active && (Vector2)transform.position == pointMove[currentIndex])
        {
            active = false;
            if (activePos == currentIndex)
            {
                objectWantToActive.GetComponent<Switch>()?.Activate(true);
                objectWantToActive.GetComponent<Lever>()?.Activate(true);
                objectWantToActive.GetComponent<Lock>()?.Activate();
            }
        }
    }

    // To change the index of movement position index
    // Change index mechanicsm is 0 -> 1 -> ... -> n -> ... -> 1 -> 0 -> ...
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
