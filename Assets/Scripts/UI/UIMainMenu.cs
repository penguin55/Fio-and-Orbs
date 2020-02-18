using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // playing the clicked sfx
    public void Clicked()
    {
        AudioManager.instance.PlayOneTime("button-click");
    }
}
