using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbBehaviour : MonoBehaviour
{
    [SerializeField] private float idleArea;
    [SerializeField] [Range(0,5)] private float timeToIdle;
    
    [SerializeField] private OrbInfo status;
    [SerializeField] private GameObject OrbsPosition;

    private Animator anim;
    
    private bool following;
    private bool idling;

    private float distance;

    private Vector2[] idleMovePoint;
    private int indexIdleMove;

    private float offsetIdleMoveArea = 0.09f;
    private float distanceIdleMovePoint;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = status.ORB_COLOR; 
        following = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        Idle();
    }

    private void LateUpdate()
    {
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
        distance = Vector2.Distance(transform.position, OrbsPosition.transform.position);

        if ( status.PLAYER.IsPlayerIdle() && !idling && distance <= idleArea)
        {
            StartCoroutine(TimeToIdle());
        }
        else if (idling && distance >= idleArea && !status.PLAYER.IsPlayerIdle())
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
}
