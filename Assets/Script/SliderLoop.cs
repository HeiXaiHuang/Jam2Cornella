using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SliderLoop : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [Tooltip("Optional parent UI GameObject to show/hide. If set, this will be used instead of toggling the slider GameObject directly.")]
    [SerializeField] private GameObject panel;
    [SerializeField] private float speed = 40f; // slider units per second
    [SerializeField] private bool playOnStart = true;

    [Header("Hit Range")]
    [Tooltip("Inclusive minimum slider value for a successful hit")]
    [SerializeField] public float hitMin = 45f;
    [Tooltip("Inclusive maximum slider value for a successful hit")]
    [SerializeField] public float hitMax = 55f;

    [Header("Hit Behavior")]
    [Header("Activation")]
    [Tooltip("Anxiety threshold above which the slider UI appears and the minigame runs")]
    [SerializeField] private float activationThreshold = 70f;

    [SerializeField] public UnityEvent onHitSuccess;
    [SerializeField] public UnityEvent onHitMiss;

    [Header("Anxiety Effects")]
    [Tooltip("Reference to the anxiety script. If set, AddAnxiety will be called on hits/misses.")]
    [SerializeField] private anxiety anxietyScript;
    [Tooltip("Amount of anxiety to reduce on success (positive). Called as AddAnxiety(-successReduce)")]
    [SerializeField] private float successReduce = 10f;
    [Tooltip("Amount of anxiety to increase on miss (positive). Called as AddAnxiety(failIncrease)")]
    [SerializeField] private float failIncrease = 5f;

    private bool increasing = true;

    void Start()
    {
        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 100f;
            // keep the component enabled so we can watch anxiety and toggle the UI
            enabled = true;
            // hide the UI initially; it will be shown when anxiety > activationThreshold
            SetUIVisible(false);
        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (slider == null) return;

        // If we have an anxiety script, only run while anxiety is above threshold.
        if (anxietyScript != null)
        {
            float current = anxietyScript.GetAnxiety();
            if (current <= activationThreshold)
            {
                // hide UI and skip updates while below or equal the threshold
                SetUIVisible(false);
                return;
            }
            else
            {
                SetUIVisible(true);
            }
        }
        else
        {
            // no anxiety script: fall back to playOnStart behavior
            if (!playOnStart)
            {
                if (slider.gameObject.activeSelf)
                    slider.gameObject.SetActive(false);
                return;
            }
            else
            {
                if (!slider.gameObject.activeSelf)
                    slider.gameObject.SetActive(true);
            }
        }

        // Space press handling: check if inside the configured hit range
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSpaceHit();
        }

        float delta = speed * Time.deltaTime;
        if (increasing)
        {
            slider.value += delta;
            if (slider.value >= slider.maxValue)
            {
                slider.value = slider.maxValue;
                increasing = false;
            }
        }
        else
        {
            slider.value -= delta;
            if (slider.value <= slider.minValue)
            {
                slider.value = slider.minValue;
                increasing = true;
            }
        }
    }

    private void CheckSpaceHit()
    {
        if (slider == null) return;

        float v = slider.value;
        bool inside = v >= Mathf.Min(hitMin, hitMax) && v <= Mathf.Max(hitMin, hitMax);

        if (inside)
        {
            // reduce anxiety when inside the hit range
            if (anxietyScript != null)
                anxietyScript.AddAnxiety(-Mathf.Abs(successReduce));
            onHitSuccess?.Invoke();
        }
        else
        {
            // increase anxiety on miss
            if (anxietyScript != null)
                anxietyScript.AddAnxiety(Mathf.Abs(failIncrease));
            onHitMiss?.Invoke();
        }
    }

    // Controls
    public void StartLoop() => enabled = true;
    public void StopLoop() => enabled = false;
    public void ResetToZero()
    {
        if (slider == null) return;
        slider.value = slider.minValue;
        increasing = true;
    }

    private void SetUIVisible(bool visible)
    {
        if (panel != null)
        {
            if (panel.activeSelf != visible)
                panel.SetActive(visible);
            return;
        }

        if (slider == null) return;

        // If this script is on the same GameObject as the slider, deactivating the slider
        // would also disable this component and stop Update(). In that case, toggle
        // visual Graphics instead so this component remains active.
        if (slider.gameObject != this.gameObject)
        {
            if (slider.gameObject.activeSelf != visible)
                slider.gameObject.SetActive(visible);
            return;
        }

        // Same GameObject: enable/disable all child Graphic components (Images, Text, etc.)
        var graphics = slider.GetComponentsInChildren<UnityEngine.UI.Graphic>(true);
        foreach (var g in graphics)
        {
            if (g != null) g.enabled = visible;
        }
    }
}
