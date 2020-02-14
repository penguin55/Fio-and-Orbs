using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    private Collider2D collider;
    private void Start()
    {
        InitializeLever();   
    }

    private void InitializeLever()
    {
        collider = GetComponent<Collider2D>();
    }

    public override void InteractsObject()
    {
        if (LevelManager.instance.hasKey && !GameVariables.FreezeGame)
        {
            Debug.Log("Has Unlocks");
            GameVariables.FreezeGame = true;
            UIManager.instance.SendMessageGameOver("Win", true);
        }
    }

    public void Activate()
    {
        collider.enabled = true;
    }
}
