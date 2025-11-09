using UnityEngine;

public class Units : MonoBehaviour
{
    public bool hasActed = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTurnForThisUnit()
    {
        hasActed = false;
    }
    
    public void FinishAction()
    {
        hasActed = true;
        
    }
}