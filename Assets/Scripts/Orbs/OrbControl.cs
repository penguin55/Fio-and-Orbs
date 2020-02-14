using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbControl : OrbBehaviour
{
    public static bool OrbsControl;

    private Vector2 directionMove;

    private void Start()
    {
        OrbsControl = false;
        Initialize();
    }

    void Update()
    {
        if (GameVariables.FreezeGame) return;
        ChangeToOrbsControl();

        if (OrbsControl)
        {
            Movement();
            Interacts();
        }
        else
        {
            LoopBehaviour();
        }
    }

    void Movement()
    {
        directionMove = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            directionMove = new Vector2(-1, directionMove.y);
        }

        if (Input.GetKey(KeyCode.W))
        {
            directionMove = new Vector2(directionMove.x, 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            directionMove = new Vector2(directionMove.x, -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            directionMove = new Vector2(1, directionMove.y);
        }

        Move(directionMove);
    }

    void ChangeToOrbsControl()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OrbsControl = !OrbsControl;
            AnimSetBool(OrbsControl);

            if (!OrbsControl)
            {
                rigid.simulated = false;
                SetFollowingOrbsStatus(true);
                Cinemachine2D.cinemachine.ChangeTarget(GetPlayer());
            } else
            {
                rigid.simulated = true;
                SetFollowingOrbsStatus(false);
                Cinemachine2D.cinemachine.ChangeTarget(transform.gameObject);
            }
        }
    }

    void Interacts()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetInteractableObject()?.InteractsObject();
        }
    }
}
