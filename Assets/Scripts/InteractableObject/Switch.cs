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

    // Activate the button switch, so the orbs can interact with this object
    public void Activate(bool status)
    {
        canInteract = status;
        GetComponent<Collider2D>().enabled = canInteract;
    }

    // To interacts this object
    public override void InteractsObject()
    {
        if (!activeSwitch) ReleaseOrbs();
    }

    // release an orbs from the switch, released orbs will be fill the diamond orbs according to its color.
    private void ReleaseOrbs()
    {
        AudioManager.instance.PlayOneTime("switch");
        LevelManager.instance.Release(orbsType);
        activeSwitch = true;
        ToggleSwitch();
        UIManager.instance.SetDiamondOrbs(orbsType);
    }

    //Just toggle the image sprite from un-pressed to pressed and vice versa.
    void ToggleSwitch()
    {
        spriteRenderer.sprite = spriteImage[(activeSwitch ? 1 : 0)];
    }

    // check the switch, is the switch can be interact or not
    public bool CanInteract()
    {
        return canInteract;
    }
}
