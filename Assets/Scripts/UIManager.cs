using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        instance = this;
    }

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

    public void SetHealth(int health)
    {
        for (int i = 0; i < healthImage.Count; i++)
        {
            if (i < health) healthImage[i].sprite = activeHP;
            else healthImage[i].sprite = deactiveHP;
        }
    }
}
