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

    // To unlock the locks and win the game
    public override void InteractsObject()
    {
        if (LevelManager.instance.hasKey && !GameVariables.FreezeGame)
        {
            AudioManager.instance.PlayOneTime("unlock");
            GameVariables.FreezeGame = true;
            UIManager.instance.SendMessageGameOver("Win", true);
        }
    }

    // To activate this behavior, when all diamond orbs and keys was collected.
    public void Activate()
    {
        collider.enabled = true;
    }
}
