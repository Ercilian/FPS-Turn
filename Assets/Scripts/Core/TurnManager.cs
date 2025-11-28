using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    [Header("Units Lists")]
    public List<Units> enemyUnits = new List<Units>();
    public List<Units> playerUnits = new List<Units>();

    public TurnBanner turnBanner;
    public bool isPlayerTurn = true;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        turnBanner.Show("YOUR TURN");
        isPlayerTurn = true;
        ResetUnits(playerUnits);
        Debug.Log("Player's Turn Started");
    }

    private void StartEnemyTurn()
    {
        turnBanner.Show("ENEMY TURN");
        isPlayerTurn = false;
        ResetUnits(enemyUnits);
        Debug.Log("Enemy's Turn Started");
    }

    private void ResetUnits(List<Units> units)
    {
        foreach (Units unit in units)
        {
            unit.ResetActions();
        }

    }

    bool AllUnitsActed(List<Units> units)
    {
        foreach (var u in units)
        {
            if (!u.hasActed)
            {
                return true;
            }
        }
        return false;
    }

    public void CheckEndTurn()
    {
        if (enemyUnits.Count == 0)
        {
            WinBattle();
            return;
        }
        if (playerUnits.Count == 0)
        {
            LoseBattle();
            return;
        }

        if (isPlayerTurn)
        {
            if (!AllUnitsActed(playerUnits))
                StartEnemyTurn();
        }
        else
        {
            if (!AllUnitsActed(enemyUnits))
                StartPlayerTurn();
        }
    }

    public void WinBattle()
    {
        turnBanner.Show("YOU WIN!");

    }

    public void LoseBattle()
    {
        turnBanner.Show("YOU LOSE!");
    }
    
}
