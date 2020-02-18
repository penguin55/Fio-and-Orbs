using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float delayCompletelyFade;

    private Slider bar;

    private bool active;

    public void Initialize(string name, float healthMax, Slider slider)
    {
        bar = slider;
        bar.transform.localScale = Vector3.one;
        bar.maxValue = healthMax;
        bar.value = healthMax;
        bar.gameObject.name = "HealthBar-" + name;
        active = false;
        bar.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (active) bar.transform.position = WorldToUISpace(HPBarManager.instance.canvas, transform.position);
    }

    // to update heath bar of enemy
    public void UpdateBar(float health)
    {
        bar.value = health;
    }

    // to set the health bar active condition. Active when the orbs is inside in collider enemy, and wi be deactive with delay after the orbs
    // move out from the collider
    public void Active()
    {
        active = true;
        bar.gameObject.SetActive(active);
    }


    // to convert the enemy position to screen and convert again to UI scale. This method will affect the health bar enemy, so that it can follow wherever the enemy move
    public Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }

    // to dissapear the health bar after the orbs move out from the colider of enemy.
    public IEnumerator Deactive()
    {
        yield return new WaitForSeconds(delayCompletelyFade);
        active = false;
        bar.gameObject.SetActive(active);
    }

    // to disable health bar after the enemy was die
    public void Disable()
    {
        bar.gameObject.SetActive(false);
    }

}
