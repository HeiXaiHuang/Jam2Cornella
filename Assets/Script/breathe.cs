using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyMinigame : MonoBehaviour
{
    [SerializeField] private anxiety anxietyScript;
    [SerializeField] private GameObject panel; // whole UI panel to show/hide
    [SerializeField] private RectTransform area; // parent rect that defines movement bounds
    [SerializeField] private RectTransform indicator; // moving indicator
    [SerializeField] private RectTransform targetZone; // zone in the middle

    [SerializeField] private float maxX = 70f;
    [SerializeField] private float minX = 10f;
    [SerializeField] private float activationThreshold = 70f;
    [SerializeField] private float speed = 300f; // pixels per second
    [SerializeField] private float reduceAmount = 10f; // how much anxiety is reduced on success
    [SerializeField] private float cooldown = 1f; // seconds after a press before next

    private bool active = false;
    private int dir = 1;

    private bool onCooldown = false;

    void Start()
    {
       
    }

    void Update()
    {
        if (anxietyScript == null) return;

        float current = anxietyScript.GetAnxiety();
        if (!active && current >= activationThreshold)
        {
            StartMinigame();
        }
        else if (active && current < activationThreshold)
        {
            StopMinigame();
        }

        if (!active) return;

        if (indicator != null)
        {
            Vector2 ap = indicator.anchoredPosition;
            ap.x += dir * speed * Time.deltaTime;
            if (ap.x < minX)
            {
                ap.x = minX;
                dir = 1;
            }
            else if (ap.x > maxX)
            {
                ap.x = maxX;
                dir = -1;
            }
            indicator.anchoredPosition = ap;
        }

        if (!onCooldown && Input.GetKeyDown(KeyCode.E))
        {
            TryCheckHit();
        }
    }

    private void StartMinigame()
    {
        active = true;
        SetPanelVisible(panel, true);
    }

    private void StopMinigame()
    {
        active = false;
        SetPanelVisible(panel, false);
    }

    private void TryCheckHit()
    {
        if (indicator == null || targetZone == null || anxietyScript == null) return;

        // indicator and targetZone should be inside the same `area` so anchoredPosition is comparable
        float indX = indicator.anchoredPosition.x;
        float targetX = targetZone.anchoredPosition.x;
        float half = targetZone.rect.width * 0.5f;

        if (Mathf.Abs(indX - targetX) <= half)
        {
            anxietyScript.AddAnxiety(-Mathf.Abs(reduceAmount));
            StartCoroutine(SuccessRoutine());
        }
        else
        {
            // small penalty for missing
            anxietyScript.AddAnxiety(Mathf.Abs(reduceAmount) * 0.2f);
            StartCoroutine(MissRoutine());
        }
    }

    private IEnumerator SuccessRoutine()
    {
        onCooldown = true;
        SetPanelVisible(panel, false);
        yield return new WaitForSeconds(cooldown);
        SetPanelVisible(panel, true);
        onCooldown = false;
    }

    // Toggles a panel's visibility. If the panel contains a SliderLoop component
    // (or components that need to keep running), toggle Graphic.enabled instead of
    // deactivating the panel so those scripts keep receiving Update().
    private void SetPanelVisible(GameObject panelObj, bool visible)
    {
        if (panelObj == null) return;

        var loops = panelObj.GetComponentsInChildren<SliderLoop>(true);
        if (loops != null && loops.Length > 0)
        {
            var graphics = panelObj.GetComponentsInChildren<UnityEngine.UI.Graphic>(true);
            foreach (var g in graphics)
            {
                if (g != null) g.enabled = visible;
            }
        }
        else
        {
            panelObj.SetActive(visible);
        }
    }

    private IEnumerator MissRoutine()
    {
        onCooldown = true;
        // optional feedback could be added here
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}