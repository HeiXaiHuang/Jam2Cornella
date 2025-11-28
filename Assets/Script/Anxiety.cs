using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class anxiety : MonoBehaviour
{
    [SerializeField] private float anxietyval = 0f;
    [SerializeField] private float maxAnxiety = 100f;
    [SerializeField] private float riseRate = 5f;
    [SerializeField] private bool autoRise = true;
    [SerializeField] private Slider anxietySlider;
    [SerializeField] private UnityEvent onAnxietyFull;

    private bool isFull = false;
    [Header("Audio Feedback")]
    [Tooltip("Optional audio clip to play while anxiety is above the threshold")]
    [SerializeField] private AudioClip anxietyClip;
    [Tooltip("Optional AudioSource to use. If not provided and anxietyClip is set, an AudioSource will be created on this GameObject.")]
    [SerializeField] private AudioSource anxietyAudioSource;
    [Tooltip("Anxiety threshold (inclusive) above which the audio starts playing")]
    [SerializeField] private float anxietySoundThreshold = 70f;

    private bool anxietySoundPlaying = false;

    void Start()
    {
        if (anxietySlider != null)
        {
            anxietySlider.minValue = 0f;
            anxietySlider.maxValue = maxAnxiety;
            anxietySlider.value = anxietyval;
        }
        // prepare audio source if needed
        // prepare audio source if needed
        if (anxietyClip != null)
        {
            if (anxietyAudioSource == null)
            {
                anxietyAudioSource = gameObject.GetComponent<AudioSource>();
                if (anxietyAudioSource == null)
                {
                    anxietyAudioSource = gameObject.AddComponent<AudioSource>();
                }
            }

            if (anxietyAudioSource != null)
            {
                anxietyAudioSource.clip = anxietyClip;
                anxietyAudioSource.loop = true;
                anxietyAudioSource.playOnAwake = false;
                anxietyAudioSource.spatialBlend = 0f; // 2D sound
                anxietyAudioSource.volume = Mathf.Max(anxietyAudioSource.volume, 1f);
                anxietyAudioSource.mute = false;
            }
        }

        UpdateAnxietyAudio();
    }

    void Update()
    {
        UpdateAnxietyAudio();
        if (isFull)
        {
            SceneManager.LoadScene("GameOver");
        }

        UpdateAnxietyAudio();
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
        UpdateAnxietyAudio();
    }

    private void UpdateAnxietyAudio()
    {
        if (anxietyAudioSource == null || anxietyClip == null)
        {
            // nothing configured
            return;
        }

        // ensure clip is assigned to the source
        if (anxietyAudioSource.clip != anxietyClip)
            anxietyAudioSource.clip = anxietyClip;

        if (anxietyval >= anxietySoundThreshold)
        {
            if (!anxietySoundPlaying)
            {
                anxietyAudioSource.Play();
                anxietySoundPlaying = true;
            }
        }
        else
        {
           
            anxietyAudioSource.Stop();
            anxietySoundPlaying = false;
        
        }
    }

    public float GetAnxiety() => anxietyval;
}
