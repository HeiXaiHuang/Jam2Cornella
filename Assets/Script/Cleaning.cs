using System.Collections;
using UnityEngine;

public class CleanAction : MonoBehaviour
{
    public GameObject objectToShow;
    public GameObject nothingToClean;
    public GameObject dialog;
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
        nothingToClean.SetActive(false);
        dialog.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (isClean == true)
            {
                Debug.Log("No hay nada que limpiar.");
                nothingToClean.SetActive(true);
                StartCoroutine(HideObjectAfterDelay(3f));
            }
            if (isClean == false)
            {
                objectToShow.SetActive(true);
                dialog.SetActive(true);
                spriteRenderer.sprite = cleanedSprite;
                isClean = true;

                if (anxietyManager != null)
                {
                    anxietyManager.AddAnxiety(-anxietyAmount);
                    Debug.Log("Anxiety modificado en: " + anxietyAmount);
                }
            }

            // Añadir ansiedad configurable al limpiar


            StartCoroutine(HideObjectAfterDelay(5f));
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
        nothingToClean.SetActive(false);
        dialog.SetActive(false);
    }
}
