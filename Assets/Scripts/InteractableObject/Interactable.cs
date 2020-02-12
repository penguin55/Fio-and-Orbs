using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactsObjectWith;
    public virtual void InteractsObject() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(interactsObjectWith))
        {
            if (interactsObjectWith.Equals("Player")) collision.GetComponent<PlayerBehaviour>().Interactable(this);
            if (interactsObjectWith.Equals("Orbs")) collision.GetComponent<OrbBehaviour>().Interactable(this);    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(interactsObjectWith))
        {
            if (interactsObjectWith.Equals("Player")) collision.GetComponent<PlayerBehaviour>().Interactable(null);
            if (interactsObjectWith.Equals("Orbs")) collision.GetComponent<OrbBehaviour>().Interactable(null);
        }
    }
}
