using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarManager : MonoBehaviour
{
    public static HPBarManager instance;
    public GameObject healthBarPrefabs;

    public Canvas canvas;

    public GameObject parentUI;

    private void Start()
    {
        instance = this;
    }

}
