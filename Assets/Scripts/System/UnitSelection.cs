using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection Instance;
    public Units selectedUnit;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!TurnManager.Instance.isPlayerTurn)
        {
            return;
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Units unit = hit.collider.GetComponent<Units>();
                
                if (unit != null && unit.isPlayerUnit && !unit.hasActed)
                {
                    SelectUnit(unit);
                }
                else
                {
                    DeselectUnit();
                }
            }
        }
    }

    private void SelectUnit(Units unit)
    {
        selectedUnit = unit;
    }
    
    public void DeselectUnit()
    {
        if (selectedUnit != null)
        {
            var playerChar = selectedUnit.GetComponent<PlayerCharacter>();
            if (playerChar != null && playerChar.targetSelectionPanel != null)
            {
                playerChar.targetSelectionPanel.SetActive(false);
            }
            selectedUnit = null;
        }
    }
}
