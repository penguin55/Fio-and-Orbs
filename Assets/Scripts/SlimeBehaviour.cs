using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWLib;

public class SlimeBehaviour : EnemyBehaviour
{
    [SerializeField] private float maxHigh;

    private List<Vector2> pointsMove;

    private bool prepareJump;
    private bool onJump;

    private int currentPointsIndex;

    public override void Initialize()
    {
        base.Initialize();
        pointsMove = new List<Vector2>();
        onPatrol = false;
        currentTime = 0;
        prepareJump = false;
        onJump = false;
    }

    public override void Move()
    {
        if (prepareJump)
        {
            prepareJump = false;
            anim.SetTrigger("Jump");
        }

        if (onJump && pointsMove.Count > 0)
        {
            Vector2 temp = TWTransform.MoveTowardsWithParabola(transform.position, ref pointsMove, movementSpeed);
            transform.position = temp;
        }
    }

    public override void Fall()
    {
        if (pointsMove.Count == currentPointsIndex && onPatrol)
        {
            anim.SetTrigger("Fall");
        }
    }

    public override void Land()
    {
        if (pointsMove.Count == 0 && onPatrol)
        {
            anim.SetTrigger("Land");
            onJump = false;
            onPatrol = false;
        }
    }

    public override void PatrolTime()
    {
        if (onPatrol) return;

        currentTime += Time.deltaTime;
        if (currentTime > timeToNextPatrol)
        {
            onPatrol = true;
            currentTime = 0;

            TWTransform.GetCurvePoints(ref pointsMove, transform.position, patrolPoints[currentPatrolIndex].position, maxHigh);
            currentPointsIndex = (int)Mathf.Floor(pointsMove.Count / 2f);
            NextPatrolIndex();
            prepareJump = true;
        }
    }

    public void LoopBehaviour()
    {
        Move();
        Fall();
        Land();
        PatrolTime();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        LoopBehaviour();
    }

    public void Jump()
    {
        onJump = true;
    }
}
