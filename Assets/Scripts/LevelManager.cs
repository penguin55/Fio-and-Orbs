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
        instance = this;
        orbsStatus = new bool[diamondOrbs.Count];
        hasKey = false;
        GenerateRandomKeyDrop();
    }

    void GenerateRandomKeyDrop()
    {
        enemy[Random.Range(0, enemy.Length)].SetDrop(dropKey);
    }

    public void Release(GameVariables.Orbs orbs)
    {
        orbsStatus[diamondOrbs.IndexOf(orbs)] = true;
        FinishCheck();
    }

    public void FinishCheck()
    {
        if (DiamondsCheck())
        {
            Debug.Log("Horray");
            PoweredUpSecretPlace();
        }
    }

    public bool DiamondsCheck()
    {
        foreach (bool flag in orbsStatus)
        {
            if (!flag) return false;
        }

        return true;
    }

    private void PoweredUpSecretPlace()
    {
        secretPlace.ActiveInteract();
    }
}
