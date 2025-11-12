using UnityEngine;

public class Units : MonoBehaviour
{
    [SerializeField] string characterName;

    public bool hasActed = true;
    bool hasAttacked = false;
    bool hasMoved = false;
    public bool isPlayerUnit;
    ClickToMove clickToMove;

    private void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        clickToMove.enabled = false;
    }


    public void Move()
    {
        if (hasActed || hasMoved)
            return;
        
        if (isPlayerUnit) 
        {
            clickToMove.enabled = true;
            Debug.Log(characterName + " is moving.");
        }
        else
        {
            Debug.Log(characterName + " an enemy unit is moving.");
        }
        
    }

    public void Attack()
    {
        if (hasActed || hasAttacked)
            return;

        Debug.Log(characterName + " is attacking.");
        
        FinishAttack();
    }

    public void PassTurn()
    {
        if (hasActed)
            return;

        Debug.Log(characterName + " is passing the turn.");

        FinishAction();
    }

    public void StartTurnForThisUnit()
    {
        hasActed = false;
        hasAttacked = false;
        hasMoved = false;
    }

    public void FinishMove()
    {
        Debug.Log(characterName + " has finished moving.");
        clickToMove.enabled = false;
        hasMoved = true;
    }

    public void FinishAttack()
    {
        hasAttacked = true;
    }
    
    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.CheckEndTurn();
        
    }
}