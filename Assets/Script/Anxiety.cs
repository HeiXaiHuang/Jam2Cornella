using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class anxiety : MonoBehaviour
{
    [SerializeField] private float anxietyval = 0f;
    [SerializeField] private float maxAnxiety = 100f;
    [SerializeField] private float riseRate = 5f;
    [SerializeField] private bool autoRise = true;
    [SerializeField] private Slider anxietySlider;
    [SerializeField] private UnityEvent onAnxietyFull;

    private bool isFull = false;

    void Start()
    {
        if (anxietySlider != null)
        {
            anxietySlider.minValue = 0f;
            anxietySlider.maxValue = maxAnxiety;
            anxietySlider.value = anxietyval;
        }
    }

    void Update()
    {
        if (isFull)
        {
            //anxiety full game over
        }
        ;

        if (autoRise && anxietyval < maxAnxiety)
        {
            anxietyval += riseRate * Time.deltaTime;
            if (anxietyval >= maxAnxiety)
            {
                anxietyval = maxAnxiety;
                isFull = true;
            }

            if (anxietySlider != null)
                anxietySlider.value = anxietyval;
        }
    }

    public void AddAnxiety(float amount)
    {
        if (isFull) return;
        anxietyval = Mathf.Clamp(anxietyval + amount, 0f, maxAnxiety);
        if (anxietyval >= maxAnxiety)
        {
            anxietyval = maxAnxiety;
            isFull = true;
        }
        if (anxietySlider != null)
            anxietySlider.value = anxietyval;
    }

    public float GetAnxiety() => anxietyval;
}
