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

    public void Initialize()
    {
        GetComponent<SpriteRenderer>().color = status.ORB_COLOR;
        following = true;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void LoopBehaviour()
    {
        FollowPlayer();
        Idle();

        IdlingAreaCheck();
    }

    void FollowPlayer()
    {
        if (following)
        {
            anim.SetBool("Idle",false);
            //transform.position = Vector2.MoveTowards(transform.position, Fio.transform.position, status.ORB_SPEED * Time.deltaTime);
            transform.position = Vector2.Lerp( transform.position,  OrbsPosition.transform.position, Time.deltaTime *  status.ORB_SPEED);
        }
    }

    void Idle()
    {
        if ( status.PLAYER.IsPlayerIdle() && idling)
        { 
            anim.SetBool("Idle",true);
        }
    }


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

    IEnumerator TimeToIdle()
    {
        yield return new WaitForSeconds(timeToIdle);
        idling = true;
        following = false;
    }

    public void ExitIdle()
    {
        StopCoroutine(TimeToIdle());
        idling = false;
        following = true;
        idleMovePoint = null;
    }

    public void SetFollowingOrbsStatus(bool status)
    {
        following = status;
    }

    public void AnimSetBool(bool status)
    {
        anim.SetBool("OrbsControl", status);
    }

    public void SetOnPlatform(bool status)
    {
        following = true;
        idling = false;
        onPlatform = status;
    }

    public GameObject GetPlayer()
    {
        return status.PLAYER.gameObject;
    }

    public void Move(Vector2 directionMove)
    {
        transform.Translate(directionMove * orbsMoveSpeed * Time.deltaTime);
    }
}
