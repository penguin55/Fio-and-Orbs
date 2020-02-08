using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public float timeToNextPatrol;
    public float movementSpeed;

    protected float currentTime;

    protected int currentPatrolIndex;
    private int multiplierPatrolIndex;

    protected Animator anim;

    protected bool onPatrol;
    
    public virtual void Initialize()
    {
        anim = GetComponent<Animator>();
        multiplierPatrolIndex = 1;
    }

    public virtual void Move() { }
    public virtual void Fall() { }
    public virtual void Land() { }

    public virtual void PatrolTime()
    {
        if (onPatrol) return;

        currentTime += Time.deltaTime;
        if (currentTime > timeToNextPatrol)
        {
            onPatrol = true;
            currentTime = 0;
        }
    }

    public void NextPatrolIndex()
    {
        currentPatrolIndex+= multiplierPatrolIndex;
        if (currentPatrolIndex >= patrolPoints.Count && multiplierPatrolIndex > 0)
        {
            multiplierPatrolIndex = -1;
            currentPatrolIndex = patrolPoints.Count - 1;
            currentPatrolIndex += multiplierPatrolIndex;
        } else if(currentPatrolIndex < 0 && multiplierPatrolIndex < 0)
        {
            multiplierPatrolIndex = 1;
            currentPatrolIndex = 0;
            currentPatrolIndex += multiplierPatrolIndex;
        }
    }

}
