using UnityEngine;

public class Units : MonoBehaviour
{
    public bool hasActed = true;
    [SerializeField] string characterName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        if (hasActed)
            return;

        Debug.Log(characterName + " is moving.");
        FinishAction();
    }

    public void Attack()
    {
        if (hasActed)
            return;

        Debug.Log(characterName + " is attacking.");
        FinishAction();
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
    }
    
    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.CheckEndTurn();
        
    }
}