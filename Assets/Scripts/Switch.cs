using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    private bool activeSwitch;

    [SerializeField] private Sprite[] spriteImage;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        activeSwitch = false;
        spriteRenderer.sprite = spriteImage[(activeSwitch ? 1 : 0)];
    }
}
