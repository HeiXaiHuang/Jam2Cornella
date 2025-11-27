using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public RectTransform creditsRect;

    private Vector2 initialPosition;

    void Awake()
    {
        if (creditsRect != null)
            initialPosition = creditsRect.anchoredPosition;
    }

    void OnEnable()
    {
        if (creditsRect != null)
        {
            creditsRect.anchoredPosition = initialPosition;
            creditsRect.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (creditsRect != null)
        {
            creditsRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            
        }
    }
}
