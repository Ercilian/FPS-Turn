using UnityEngine;
using UnityEngine.UI;

public class UiUnitSelectionToggle : MonoBehaviour
{
    [SerializeField] GameObject[] elementUIToToggle;

    void Update()
    {
        Units selectedUnit = UnitSelection.Instance.selectedUnit;
        string unitSelection = selectedUnit != null ? selectedUnit.name : null;

        switch (unitSelection)
        {
            case null:
                DeactivateAllUIElements();
                break;

            case "Ellen":
                DeactivateAllUIElements();
                if (elementUIToToggle.Length > 0)
                {
                    elementUIToToggle[0].SetActive(true);
                    UpdateActionButtons(elementUIToToggle[0], selectedUnit);
                }
                break;

            case "Grenadier Ally":
                DeactivateAllUIElements();
                if (elementUIToToggle.Length > 1)
                {
                    elementUIToToggle[1].SetActive(true);
                    UpdateActionButtons(elementUIToToggle[1], selectedUnit);
                }
                break;

            default:
                break;
        }
    }

    void DeactivateAllUIElements()
    {
        for (int i = 0; i < elementUIToToggle.Length; i++)
        {
            elementUIToToggle[i].SetActive(false);
        }
    }

    void UpdateActionButtons(GameObject panel, Units unit)
    {
        var runButton = panel.transform.Find("ButtonMove")?.GetComponent<Button>();
        var attackButton = panel.transform.Find("ButtonAttack")?.GetComponent<Button>();
        var passButton = panel.transform.Find("ButtonPassTurn")?.GetComponent<Button>();

        if (runButton != null)
            runButton.interactable = !unit.HasMoved;

        if (attackButton != null)
            attackButton.interactable = !unit.HasAttacked;
        if (passButton != null)
            passButton.interactable = !unit.HasActed;
            
    }
}
