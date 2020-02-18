using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Diamonds Orbs")]
    public List<Image> diamondOrbsImage;
    public DiamondOrbs diamondSpriteData;

    [Header("Health")]
    public List<Image> healthImage;
    public Sprite activeHP;
    public Sprite deactiveHP;

    [Header("Game Over Panel")] 
    public GameObject gameOverPanel;
    public GameObject losePanel;
    public GameObject winPanel;

    private void Start()
    {
        instance = this;
    }

    // update diamond orbs
    public void SetDiamondOrbs(GameVariables.Orbs orbsType)
    {
        switch (orbsType)
        {
            case GameVariables.Orbs.RED:
                diamondOrbsImage[0].sprite = diamondSpriteData.GetSprite(orbsType, true);
                break;
            case GameVariables.Orbs.YELLOW:
                diamondOrbsImage[1].sprite = diamondSpriteData.GetSprite(orbsType, true);
                break;
            case GameVariables.Orbs.GREEN:
                diamondOrbsImage[2].sprite = diamondSpriteData.GetSprite(orbsType, true);
                break;
            case GameVariables.Orbs.BLUE:
                diamondOrbsImage[3].sprite = diamondSpriteData.GetSprite(orbsType, true);
                break;
        }
    }

    // update health player
    public void SetHealth(int health)
    {
        for (int i = 0; i < healthImage.Count; i++)
        {
            if (i < health) healthImage[i].sprite = activeHP;
            else healthImage[i].sprite = deactiveHP;
        }
    }

    // To send a message to UI Manager from another script, if the game is met a condition of win or lose
    public void SendMessageGameOver(string command, bool flag)
    {
        switch (command.ToLower())
        {
            case "lose" :
                Lose(flag);
                break;
            case "win" :
                Win(flag);
                break;
        }
    }

    // to enable lose's panel when the win condition met
    private void Lose(bool status)
    {
        gameOverPanel.SetActive(status);
        losePanel.SetActive(status);
    }
    
    // to enable win's panel when the win condition met
    private void Win(bool status)
    {
        gameOverPanel.SetActive(status);
        winPanel.SetActive(status);
    }

    // playing the clicked sfx
    public void Clicked()
    {
        AudioManager.instance.PlayOneTime("button-click");
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
