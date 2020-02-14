using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private GameVariables.Items _itemsType;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_itemsType == GameVariables.Items.KEY) LevelManager.instance.hasKey = true;
            Destroy(gameObject);
        }
    }
}
