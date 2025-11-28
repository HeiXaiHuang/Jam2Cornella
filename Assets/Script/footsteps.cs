using UnityEngine;

[RequireComponent(typeof(Transform))]
public class footsteps : MonoBehaviour
{
    [Header("Audio")]
    [Tooltip("Footstep audio clips to play")]
    [SerializeField] private AudioClip[] footstepClips;
    [Tooltip("AudioSource used to play footstep sounds. If empty, one will be created on Start.")]
    [SerializeField] private AudioSource audioSource;

    [Header("Timing & Movement")]
    [Tooltip("Minimum movement speed (units/second) to consider as walking")]
    [SerializeField] private float minMoveThreshold = 0.1f;
    [Tooltip("Time between footstep sounds while moving (seconds)")]
    [SerializeField] private float stepInterval = 0.5f;

    [Header("Pitch & Selection")]
    [SerializeField] private bool randomizePitch = true;
    [SerializeField] private Vector2 pitchRange = new Vector2(0.95f, 1.05f);
    [SerializeField] private bool randomizeClip = true;

    private Vector3 lastPosition;
    private float stepTimer = 0f;

    void Start()
    {
        lastPosition = transform.position;
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.loop = false;
            }
        }
    }

    void Update()
    {
        // measure movement speed
        Vector3 delta = transform.position - lastPosition;
        float speed = 0f;
        if (Time.deltaTime > 0f)
            speed = delta.magnitude / Time.deltaTime;

        lastPosition = transform.position;

        stepTimer += Time.deltaTime;

        if (speed >= minMoveThreshold)
        {
            if (stepTimer >= stepInterval)
            {
                PlayStep();
                stepTimer = 0f;
            }
        }
        else
        {
            // reset timer when not moving so steps don't queue
            stepTimer = Mathf.Min(stepTimer, stepInterval);
            // stop any currently playing footstep sound when we stop moving
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    /// <summary>
    /// Play a single footstep sound using the configured clips and audio source.
    /// </summary>
    public void PlayStep()
    {
        if (audioSource == null || footstepClips == null || footstepClips.Length == 0) return;

        AudioClip clip = footstepClips[0];
        if (randomizeClip && footstepClips.Length > 1)
        {
            int idx = Random.Range(0, footstepClips.Length);
            clip = footstepClips[idx];
        }

        float prevPitch = audioSource.pitch;
        if (randomizePitch)
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);

        // use the audioSource.clip so we can stop playback when movement stops
        audioSource.clip = clip;
        audioSource.Play();

        // restore pitch so other audio on this source is unaffected
        audioSource.pitch = prevPitch;
    }

    // Optional helpers
    public void SetClips(AudioClip[] clips) => footstepClips = clips;
    public void SetAudioSource(AudioSource src) => audioSource = src;
}

