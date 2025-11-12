using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection Instance;

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
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Units unit = hit.collider.GetComponent<Units>();
                if (unit != null && unit.isPlayerUnit && !unit.hasActed)
                {
                    SelectUnit();
                    Debug.Log("Unit Selected: " + unit.name);
                    // Aquí puedes agregar lógica adicional para seleccionar la unidad
                }
                else { Debug.Log("No unit found at clicked position."); }
            }
        }
    }
    
    private void SelectUnit()
    {
        // Lógica para seleccionar la unidad
    }
}
