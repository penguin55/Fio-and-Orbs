using UnityEngine;

[CreateAssetMenu(menuName = "Create Diamond Data", fileName = "Diamond Orbs")]
public class DiamondOrbs : ScriptableObject
{
    public Sprite[] diamondOrbsSprite;

    public Sprite GetSprite(GameVariables.Orbs orbsType, bool flag)
    {
        switch (orbsType)
        {
            case GameVariables.Orbs.RED :
                return diamondOrbsSprite[flag ? 1 : 0];
            case GameVariables.Orbs.YELLOW:
                return diamondOrbsSprite[flag ? 3 : 2];
            case GameVariables.Orbs.GREEN:
                return diamondOrbsSprite[flag ? 5 : 4];
            case GameVariables.Orbs.BLUE:
                return diamondOrbsSprite[flag ? 7 : 6];
            default:
                return null;
        }
    }
}
