using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    private bool activeSwitch;

    [SerializeField] private GameVariables.Orbs orbsType;

    [SerializeField] private bool canInteract;

    [SerializeField] private Sprite[] spriteImage;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        activeSwitch = false;
        Activate(canInteract);
        ToggleSwitch();
    }

    public void Activate(bool status)
    {
        canInteract = status;
        GetComponent<Collider2D>().enabled = canInteract;
    }

    public override void InteractsObject()
    {
        ReleaseOrbs();
    }

    private void ReleaseOrbs()
    {
        LevelManager.instance.Release(orbsType);
        activeSwitch = true;
        ToggleSwitch();
        UIManager.instance.SetDiamondOrbs(orbsType);
    }

    void ToggleSwitch()
    {
        spriteRenderer.sprite = spriteImage[(activeSwitch ? 1 : 0)];
    }

    public bool CanInteract()
    {
        return canInteract;
    }
}
