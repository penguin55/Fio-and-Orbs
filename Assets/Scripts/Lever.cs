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
        try
        {
            (leverGenerator[index].GetComponent<Bridge>()).ActiveInteract();
        } 
        catch (UnassignedReferenceException ex)
        {
            
        }
        catch (NullReferenceException nullEx)
        {
            
        }
    }

    void OnDeactiveGenerator(int index)
    {
        try
        {
            (leverGenerator[index].GetComponent<Bridge>()).DeactiveInteract();
        }
        catch (UnassignedReferenceException ex)
        {

        }
        catch (NullReferenceException nullEx)
        {

        }
    }
}
