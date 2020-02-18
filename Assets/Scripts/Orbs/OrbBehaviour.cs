using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbBehaviour : MonoBehaviour
{
    [SerializeField] private float idleArea;
    [SerializeField] [Range(0,5)] private float timeToIdle;
    
    [SerializeField] private OrbInfo status;
    [SerializeField] private GameObject OrbsPosition;

    [SerializeField] private float orbsMoveSpeed;

    private Animator anim;
    protected Rigidbody2D rigid;
    
    private bool following;
    private bool idling;

    private bool onPlatform;

    private float distance;

    private Vector2[] idleMovePoint;
    private int indexIdleMove;

    private float offsetIdleMoveArea = 0.09f;
    private float distanceIdleMovePoint;

    private Interactable interactableObject;

    public void Initialize()
    {
        GetComponent<SpriteRenderer>().color = status.ORB_COLOR;
        following = true;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // just want to make the method more solid
    public void LoopBehaviour()
    {
        FollowPlayer();
        Idle();

        IdlingAreaCheck();
    }

    // Make the orbs can following the player
    void FollowPlayer()
    {
        if (following || status.PLAYER.IsPlayerIdle() || idling || onPlatform)
        {
            anim.SetBool("Idle",false);
            //transform.position = Vector2.MoveTowards(transform.position, Fio.transform.position, status.ORB_SPEED * Time.deltaTime);
            transform.position = Vector2.Lerp( transform.position,  OrbsPosition.transform.position, Time.deltaTime *  status.ORB_SPEED);
        }
    }

    void Idle()
    {
        if ( status.PLAYER.IsPlayerIdle() && idling && !onPlatform)
        { 
            anim.SetBool("Idle",true);
        }
    }

    // To check if the orbs in idle area that were we specify and player is in idle condition. If all condition are met  then orbs will animate idle animation
    void IdlingAreaCheck()
    {
        if (onPlatform) return;

        distance = Vector2.Distance(transform.position, OrbsPosition.transform.position);

        if ( status.PLAYER.IsPlayerIdle() && !idling && distance <= idleArea)
        {
            StartCoroutine(TimeToIdle());
        }
        else if ( (idling && distance >= idleArea) || !status.PLAYER.IsPlayerIdle())
        {
            ExitIdle();
        }
    }

    // Delay from following condition to idle condition
    IEnumerator TimeToIdle()
    {
        yield return new WaitForSeconds(timeToIdle);
        idling = true;
        following = false;
    }

    // Exit idle animation
    public void ExitIdle()
    {
        StopCoroutine(TimeToIdle());
        idling = false;
        following = true;
        idleMovePoint = null;
    }

    // to set the following condition
    public void SetFollowingOrbsStatus(bool status)
    {
        following = status;
    }

    public void AnimSetBool(bool status)
    {
        anim.SetBool("OrbsControl", status);
    }

    // This method will execute when the player is on the moving platform, so the orbs will be have same parent with player object.
    public void SetOnPlatform(bool status)
    {
        following = status;
        idling = !status;
        onPlatform = status;
        StopCoroutine(TimeToIdle());
    }

    // To set objects will be interact with orbs when the orbs is inside collider of the interactable object such as switch button.
    public void Interactable(Interactable interacts)
    {
        interactableObject = interacts;
    }

    // To get what object was active to interact with orbs
    public Interactable GetInteractableObject()
    {
        return interactableObject;
    }

    public GameObject GetPlayer()
    {
        return status.PLAYER.gameObject;
    }

    public void Move(Vector2 directionMove)
    {
        transform.Translate(directionMove * orbsMoveSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().DamageTaken(status.DAMAGE);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().StopDamageTaken();
        }
    }
}
