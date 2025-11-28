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

        if (!onCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            TryCheckHit();
        }
    }

    private void StartMinigame()
    {
        active = true;
        if (panel != null) panel.SetActive(true);
    }

    private void StopMinigame()
    {
        active = false;
        if (panel != null) panel.SetActive(false);
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
        if (panel != null) panel.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        if (panel != null) panel.SetActive(true);
        onCooldown = false;
    }

    private IEnumerator MissRoutine()
    {
        onCooldown = true;
        // optional feedback could be added here
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
