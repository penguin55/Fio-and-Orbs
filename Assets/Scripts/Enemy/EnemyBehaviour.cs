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

    protected bool takingDamage;
    private Coroutine takingDamageCoroutine;
    protected bool dead;

    protected Coroutine showHPBarCoroutine;
    protected bool HPBarShowed;

    public virtual void Initialize()
    {
        anim = GetComponent<Animator>();
        multiplierPatrolIndex = 1;

        Slider slider = Instantiate(HPBarManager.instance.healthBarPrefabs).GetComponent<Slider>();
        slider.gameObject.transform.parent = HPBarManager.instance.parentUI.transform;
        healthBar.Initialize("Slime", status.HEALTH, slider);
        takingDamage = false;
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

    void Dead()
    {
        dead = true;
        StopDamageTaken();
        anim.SetTrigger("Dead");
    }

    public void AfterAnimationDead()
    {
        healthBar.Disable();
        transform.parent.gameObject.SetActive(false);
    }

    public void DamageTaken(float damage)
    {
        if (status.HEALTH > 0 && !takingDamage)
        {
            takingDamage = true;
            takingDamageCoroutine = StartCoroutine(TakingDamage(damage));
        } 
    }

    public void StopDamageTaken()
    {
        takingDamage = false;
        if (takingDamageCoroutine != null) StopCoroutine(takingDamageCoroutine);
        takingDamageCoroutine = null;
    }

    IEnumerator TakingDamage(float damage)
    {
        while (true)
        {
            UpdateHealth(Time.deltaTime * damage);
            yield return new WaitForEndOfFrame();
        }
    }

    void UpdateHealth(float health)
    {
        status.HEALTH -= health;

        if (status.HEALTH < 0)
        {
            status.HEALTH = 0;
            Dead();
        }

        healthBar.UpdateBar(status.HEALTH);
    }

}

[System.Serializable]
public class EnemyStatus
{
    public string NAME;
    public float HEALTH;
    public float MOVEMENT_SPEED;
}
