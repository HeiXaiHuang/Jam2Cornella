using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SliderLoop : MonoBehaviour
{
    [SerializeField] private Slider slider;
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
            if (playOnStart)
                enabled = true;
            else
                enabled = false;
        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (slider == null) return;

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
}
