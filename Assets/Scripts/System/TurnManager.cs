using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance; // Create a singleton instance
    public bool isPlayerTurn = true; // Variable to track whose turn it is

    public List<Units> enemyUnits = new List<Units>();
    public List<Units> playerUnits = new List<Units>();

    public TurnBanner turnBanner;


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
        StartCoroutine(EnemyTurnDelay());
    }

    private void ResetUnits(List<Units> units)
    {
        foreach (Units unit in units)
        {
            unit.hasActed = false;
            // Aquí puedes resetear el estado de cada unidad para el nuevo turno
            // Por ejemplo, restablecer puntos de acción, mover estado, etc.
        }

    }

    bool AllUnitsActed(List<Units> units)
    {
        foreach (var u in units) //var u representa cada unidad en la lista units //también se puede usar Units u
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
        if (isPlayerTurn)
        {
            if (!AllUnitsActed(playerUnits)) //si todas las unidades del jugador han actuado
                StartEnemyTurn();
        }
        else
        {
            if (!AllUnitsActed(enemyUnits)) //si todas las unidades enemigas han actuado
                StartPlayerTurn();
        }
    }
    
    private IEnumerator EnemyTurnDelay()
    {
        yield return new WaitForSeconds(5f);
        StartPlayerTurn();
    }
}