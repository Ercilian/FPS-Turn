using Unity.VisualScripting;
using UnityEngine;

public class Units : MonoBehaviour
{
    [Header("Unit Properties")]
    [SerializeField] string characterName;
    public bool hasActed = true;
    bool hasAttacked = false;
    [SerializeField] bool hasMoved = false;
    public bool isPlayerUnit;
    
    Shooting Shooting;
    ClickToMove clickToMove;
    PlayerCharacter playerCharacter;
    public float rangopuesto = 10f;
    
    
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
        if (isPlayerUnit)
        {
            clickToMove.enabled = false;
        }
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rangopuesto, Color.green);   
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
            if (playerCharacter.targetSelectionPanel != null)
            {
                playerCharacter.targetSelectionPanel.SetActive(true);

                var tsa = playerCharacter.targetSelectionPanel.GetComponent<TargetSelectionAttack>();
                if (tsa != null)
                {
                    tsa.UpdateAttackButtons();
                }
                else
                {
                    Debug.LogWarning("TargetSelectionAttack no encontrado en el panel de selección.");
                }
            }
            else
            {
                Debug.LogWarning("targetSelectionPanel no está asignado en PlayerCharacter.");
            }

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
        if (isPlayerUnit)
        {
            clickToMove.enabled = false;

            if (playerCharacter.targetSelectionPanel != null && playerCharacter.targetSelectionPanel.activeSelf)
            {
                var tsa = playerCharacter.targetSelectionPanel.GetComponent<TargetSelectionAttack>();
                if (tsa != null)
                {
                    tsa.UpdateAttackButtons();
                }
            }
        }
        hasMoved = true;
        CheckIfFinishTurn();
    }

    public void FinishAttack()
    {
        Debug.Log(characterName + " has finished attacking.");
        if (isPlayerUnit)
        {
            playerCharacter.targetSelectionPanel.SetActive(false);
        }
        hasAttacked = true;
        CheckIfFinishTurn();
    }
    
    public void FinishAction()
    {
        if (isPlayerUnit)
        {
            UnitSelection.Instance.DeselectUnit();
        }
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
