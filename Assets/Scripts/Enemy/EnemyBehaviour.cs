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

    protected GameObject drops;

    public virtual void Initialize()
    {
        anim = GetComponent<Animator>();
        multiplierPatrolIndex = 1;

        Slider slider = Instantiate(HPBarManager.instance.healthBarPrefabs).GetComponent<Slider>();
        slider.gameObject.transform.parent = HPBarManager.instance.parentUI.transform;
        healthBar.Initialize("Slime", status.HEALTH, slider);
        takingDamage = false;
    }

    //To set the drop item, drop item will be instantiate when enemy die
    public void SetDrop(GameObject drops)
    {
        this.drops = drops;
    }

    public virtual void Move() { }
    public virtual void Fall() { }
    public virtual void Land() { }

    // To count patrol time from currentpoint to next point, when the time is more than or equal than timeToNextPatrol
    // Moving to next position will begin
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

    // To change the index of patro time, I use List to save the patrol point so I must define where is teh index patrol
    // This method works to move the next index, but if the index is equal than length of patrol point List, the current index will be
    // move to the left not be 0 again
    // For example length of patrol point is 3 then 0 - > 1 -> 2 -> 1 -> 0 - > 1 ....
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


    // To handle drop item when the animation enemy is done, when the animtion done, this method wil be called
    // and item will be dropped
    public void AfterAnimationDead()
    {
        healthBar.Disable();
        transform.parent.gameObject.SetActive(false);
        DropItem();
    }

    // This methdo handling instantiate the drop item when the drop item variable assign or not null
    void DropItem()
    {
        if (drops)
        {
            Instantiate(drops, hPBarPosition.position, Quaternion.identity);
        }
    }

    // to reduce the enemy's HP when takin a damage
    // Taking damage will be always decrement, untill the orbs move out from collider area of slime
    // This method wil start the Start Coroutine to always taking damage when the orbs sti inside the slime collider
    public void DamageTaken(float damage)
    {
        if (status.HEALTH > 0 && !takingDamage)
        {
            takingDamage = true;
            takingDamageCoroutine = StartCoroutine(TakingDamage(damage));
        } 
    }

    // The method to stop the coroutine of taking damage, it will happen when the orbs move out from slime's collider
    public void StopDamageTaken()
    {
        takingDamage = false;
        if (takingDamageCoroutine != null) StopCoroutine(takingDamageCoroutine);
        takingDamageCoroutine = null;
    }

    // IEnumerator to make slime always taking damage when this Ienumerator called
    IEnumerator TakingDamage(float damage)
    {
        while (true)
        {
            UpdateHealth(Time.deltaTime * damage);
            yield return new WaitForEndOfFrame();
        }
    }

    // to update the health bar of enemy by accessing the healtbar ui
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


// an Abstract Data Type to save enemy info
[System.Serializable]
public class EnemyStatus
{
    public string NAME;
    public float HEALTH;
    public float MOVEMENT_SPEED;
}
