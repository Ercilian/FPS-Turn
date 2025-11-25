using Unity.VisualScripting;
using UnityEngine;

public class Units : MonoBehaviour
{
    [Header("Unit Properties")]
    [SerializeField] string characterName;
    public bool hasActed = true;
    bool hasAttacked = false;
    bool hasMoved = false;
    public bool isPlayerUnit;
    
    Shooting Shooting;
    ClickToMove clickToMove;
    PlayerCharacter playerCharacter;
    
    public string CharacterName => characterName;
    public bool HasMoved => hasMoved;
    public bool HasAttacked => hasAttacked;
    public bool HasActed => hasActed;

    private void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        Shooting = GetComponent<Shooting>();
        playerCharacter = GetComponent<PlayerCharacter>();
        
        Shooting.enabled = false;
        clickToMove.enabled = false;
    }


    public void Move()
    {
        if (hasActed || hasMoved)
            return;
        
        if (isPlayerUnit) 
        {
            clickToMove.EnableMoveMode();
            clickToMove.destinationDummie.position = transform.position;
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

        if (isPlayerUnit)
        {
            playerCharacter.targetSelectionPanel.SetActive(true);
            Shooting.enabled = true;
            Debug.Log(characterName + " ally is selecting a target.");
        }
        else
        {
            Debug.Log(characterName + " an enemy unit is selecting a target.");
        }
        
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
        Debug.Log("HasMoved set to true for " + characterName);
        CheckIfFinishTurn();
    }

    public void FinishAttack()
    {
        if (isPlayerUnit)
        {
            playerCharacter.targetSelectionPanel.SetActive(false);
        }
        hasAttacked = true;
        CheckIfFinishTurn();
    }
    
    public void FinishAction()
    {
        hasActed = true;
        Debug.Log(characterName + " has finished its turn.");
        TurnManager.Instance.CheckEndTurn();
        
    }

    void CheckIfFinishTurn()
    {
        if (hasMoved && hasAttacked)
        {
            FinishAction();
        }
    }

    public void ResetActions()
    {
        hasMoved = false;
        hasAttacked = false;
        hasActed = false;
    }
}
