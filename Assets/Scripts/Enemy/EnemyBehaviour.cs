using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public EnemyStatus status;
    public float timeToNextPatrol;

    public Transform hPBarPosition;
    [SerializeField] protected HealthBar healthBar;

    protected float currentTime;

    protected int currentPatrolIndex;
    private int multiplierPatrolIndex;

    protected Animator anim;

    protected bool onPatrol;
    
    public virtual void Initialize()
    {
        anim = GetComponent<Animator>();
        multiplierPatrolIndex = 1;

        Slider slider = Instantiate(HPBarManager.instance.healthBarPrefabs).GetComponent<Slider>();
        slider.gameObject.transform.parent = HPBarManager.instance.parentUI.transform;
        healthBar.Initialize("Slime", 100, slider);
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

[System.Serializable]
public class EnemyStatus
{
    public string NAME;
    public int HEALTH;
    public float MOVEMENT_SPEED;
}
