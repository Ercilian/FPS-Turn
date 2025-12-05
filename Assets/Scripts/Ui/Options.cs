using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Options : MonoBehaviour
{
    public GameObject OptionsPanel;

    private void Start()
    {
        OptionsPanel.SetActive(false);
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb != null && kb.escapeKey.wasPressedThisFrame)
        {
            if (OptionsPanel.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                OpenSettings();
            }
        }
    }

    public void OpenSettings()
    {
        OptionsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        OptionsPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
