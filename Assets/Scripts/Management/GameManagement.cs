using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    // Set the application to 60 for max frame rate 
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
