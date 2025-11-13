using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TurnBanner : MonoBehaviour
{
    [Header("Refs")]
    public CanvasGroup canvasGroup;
    public RectTransform panel;
    public TextMeshProUGUI title;

    [Header("Anim Settings")]
    public float slideDistance = 150f;
    public float animSpeed = 5f;
    public float visibleTime = 1.8f;
    public bool autoHide = true;

    Vector2 initialPos;

    void Awake()
    {
        initialPos = panel.anchoredPosition;

        canvasGroup.alpha = 0f;
        panel.anchoredPosition = initialPos + Vector2.up * slideDistance;

    }

    public void Show(string mainText = "YOUR TURN", string subText = "")
    {
        title.text = mainText;

        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * animSpeed;
            canvasGroup.alpha = Mathf.SmoothStep(0f, 1f, t);
            panel.anchoredPosition = Vector2.Lerp(initialPos + Vector2.up * slideDistance, initialPos, t);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        panel.anchoredPosition = initialPos;

        if (autoHide)
        {
            yield return new WaitForSeconds(visibleTime);
            t = 1f;
            while (t > 0f)
            {
                t -= Time.deltaTime * animSpeed;
                canvasGroup.alpha = Mathf.SmoothStep(0f, 1f, t);
                panel.anchoredPosition = Vector2.Lerp(initialPos + Vector2.up * slideDistance, initialPos, t);
                yield return null;
            }

            canvasGroup.alpha = 0f;
            panel.anchoredPosition = initialPos + Vector2.up * slideDistance;
        }
    }
    
    public void HideImmediate()
    {
        StopAllCoroutines();
        canvasGroup.alpha = 0f;
        panel.anchoredPosition = initialPos + Vector2.up * slideDistance;
    }
}
