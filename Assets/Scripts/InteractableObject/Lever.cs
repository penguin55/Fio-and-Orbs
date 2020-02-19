using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] private int currentStatusLever;
    private int statusLever;

    private int multiplierStatus;

    [SerializeField] private Sprite[] leverImageStatus;
    [SerializeField] private GameObject[] leverGenerator;

    private SpriteRenderer leverSprite;

    private void Start()
    {
        InitializeLever();   
    }

    private void InitializeLever()
    {
        leverSprite = GetComponent<SpriteRenderer>();
        statusLever = currentStatusLever;
        leverSprite.sprite = leverImageStatus[statusLever];
        multiplierStatus = multiplierStatus == (leverGenerator.Length - 1) ? -1 : 1; 
        OnActiveGenerator(statusLever);
    }

    // To interacts the lever to active condition or to deactive condition
    public override void InteractsObject()
    {
        AudioManager.instance.PlayOneTime("lever");
        OnDeactiveGenerator(statusLever);
        ChangeStatusLever();
        OnActiveGenerator(statusLever);
    } 

    // Change the sprite state according to current status lever. Status lever is depend on what we input in inspector
    private void ChangeStatusLever()
    {
        statusLever += multiplierStatus;

        if (statusLever >= leverGenerator.Length)
        {
            statusLever = leverGenerator.Length - 1;
            multiplierStatus = -1;

            statusLever += multiplierStatus;
        } else if (statusLever < 0)
        {
            statusLever = 0;
            multiplierStatus = 1;

            statusLever += multiplierStatus;
        }

        leverSprite.sprite = leverImageStatus[statusLever];
    }

    // To activated the generator which associates with this script, such as activate the bridge movement or switch button movement
    void OnActiveGenerator(int index)
    {
        if (leverGenerator[index])
        {
            (leverGenerator[index].GetComponent<Bridge>())?.ActiveInteract();
            (leverGenerator[index].GetComponent<StepMover>())?.ActiveInteract();
        }
    }

    // To deactivated the generator which associates with this script, such as deactivate the bridge or switch button movement
    void OnDeactiveGenerator(int index)
    {
        if (leverGenerator[index])
        {
            (leverGenerator[index].GetComponent<Bridge>())?.DeactiveInteract();
            (leverGenerator[index].GetComponent<StepMover>())?.DeactiveInteract();
        }
    }

    // To activate the lever when generator power up this lever. Not just a bridge can be powered up and activated, the lever is also can have the same behaviour
    public void Activate(bool flag)
    {
        GetComponent<Collider2D>().enabled = flag;
    }
}
