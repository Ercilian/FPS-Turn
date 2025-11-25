using UnityEngine;
using TMPro;
using System.Collections;

public class UISelectedUnit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI unitNameText;
    [SerializeField] GameObject panel;
    Units lastSelectedUnit;


    void Update()
    {
        var selectedUnit = UnitSelection.Instance.selectedUnit;
        if (selectedUnit != lastSelectedUnit)
        {
            lastSelectedUnit = selectedUnit;
            if (selectedUnit != null)
            {
                unitNameText.text = selectedUnit.CharacterName;
                panel.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(HideBannerAfterDelay());
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }

    IEnumerator HideBannerAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }
}
