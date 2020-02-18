using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private List<GameVariables.Orbs> diamondOrbs;

    [SerializeField] private bool[] orbsStatus;
    public bool hasKey;

    [SerializeField] private EnemyBehaviour[] enemy;
    [SerializeField] private GameObject dropKey;

    [SerializeField] private StepMover secretPlace;

    private void Start()
    {
        GameVariables.FreezeGame = false;
        instance = this;
        orbsStatus = new bool[diamondOrbs.Count];
        hasKey = false;
        GenerateRandomKeyDrop();
    }

    // to generate key drop from enemy in scene. Only an enemy can bring the key.
    void GenerateRandomKeyDrop()
    {
        enemy[Random.Range(0, enemy.Length)].SetDrop(dropKey);
    }

    // To save the diamond orbs status depending on what has been obtained
    public void Release(GameVariables.Orbs orbs)
    {
        orbsStatus[diamondOrbs.IndexOf(orbs)] = true;
        FinishCheck();
    }

    // To check if all diamond orbs were collected or not, when all diamond orbs were collected then the lock will move appear and can now be interacted
    public void FinishCheck()
    {
        if (DiamondsCheck())
        {
            Debug.Log("Horray");
            PoweredUpSecretPlace();
        }
    }

    // To check the status of diamond orbs. If true the diamond orbs were collected, else the diamond orbs stil missing
    public bool DiamondsCheck()
    {
        foreach (bool flag in orbsStatus)
        {
            if (!flag) return false;
        }

        return true;
    }

    // Make the lock appear to the scene and can now be interacted by player
    private void PoweredUpSecretPlace()
    {
        secretPlace.ActiveInteract();
    }
}
