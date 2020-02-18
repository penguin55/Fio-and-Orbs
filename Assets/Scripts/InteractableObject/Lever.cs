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

    public override void InteractsObject()
    {
        AudioManager.instance.PlayOneTime("lever");
        OnDeactiveGenerator(statusLever);
        ChangeStatusLever();
        OnActiveGenerator(statusLever);
    } 

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

    void OnActiveGenerator(int index)
    {
        if (leverGenerator[index])
        {
            (leverGenerator[index].GetComponent<Bridge>())?.ActiveInteract();
            (leverGenerator[index].GetComponent<StepMover>())?.ActiveInteract();
        }
    }

    void OnDeactiveGenerator(int index)
    {
        if (leverGenerator[index])
        {
            (leverGenerator[index].GetComponent<Bridge>())?.DeactiveInteract();
            (leverGenerator[index].GetComponent<StepMover>())?.DeactiveInteract();
        }
    }
    
    public void Activate(bool flag)
    {
        GetComponent<Collider2D>().enabled = flag;
    }
}
