using System.Collections;
using UnityEngine;

public class CleanAction : MonoBehaviour
{
    public GameObject objectToShow;
    public SpriteRenderer spriteRenderer;
    public Sprite cleanedSprite;

    bool isPlayerInside = false;
    public bool isClean = false;

    public anxiety anxietyManager;
    [Header("Anxiety Settings")]
    [Range(-50f, 50f)]
    public float anxietyAmount = 30f;

    void Start()
    {
        objectToShow.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && !isClean && Input.GetKeyDown(KeyCode.E))
        {
            objectToShow.SetActive(true);
            spriteRenderer.sprite = cleanedSprite;
            isClean = true;

            // Añadir ansiedad configurable al limpiar
            if (anxietyManager != null)
            {
                anxietyManager.AddAnxiety(-anxietyAmount);
                Debug.Log("Anxiety modificado en: " + anxietyAmount);
            }

            StartCoroutine(HideObjectAfterDelay(3f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInside = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInside = false;
    }

    IEnumerator HideObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        objectToShow.SetActive(false);
    }
}
