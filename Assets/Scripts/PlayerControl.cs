using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : PlayerBehaviour
{
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        ControllerMove();
    }

    private void FixedUpdate()
    {
        ControllerJump();
    }

    void ControllerMove()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }

        Walk(direction);
    }

    void ControllerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}
