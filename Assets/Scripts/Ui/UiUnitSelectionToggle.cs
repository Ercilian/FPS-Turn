using UnityEngine;

public class UiUnitSelectionToggle : MonoBehaviour
{

    [SerializeField] GameObject[] elementUIToToggle;

    void Start()
    {
        
    }

    void Update()
    {
        string unitSelection = UnitSelection.Instance.selectedUnit != null
            ? UnitSelection.Instance.selectedUnit.name
            : null;

        switch (unitSelection)
        {
            case null:
                for (int i = 0; i < elementUIToToggle.Length; i++)
                {
                    elementUIToToggle[i].SetActive(false);
                }
                break;

            case "Ellen":
                DeactivateAllUIElements();
                if (elementUIToToggle.Length > 0)
                elementUIToToggle[0].SetActive(true);
                break;

            case "Grenadier Ally":
                DeactivateAllUIElements();
                if (elementUIToToggle.Length > 1)
                elementUIToToggle[1].SetActive(true);
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
}
