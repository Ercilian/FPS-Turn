using UnityEngine;
using UnityEngine.UI;

public class UnitActionPanel : MonoBehaviour
{
    [SerializeField] Button buttonRun;
    [SerializeField] Button buttonAttack;

    public void UpdateButtons(Units selectedUnit)
    {
        buttonRun.interactable = !selectedUnit.HasMoved;
        buttonAttack.interactable = !selectedUnit.HasAttacked;
    }
}
