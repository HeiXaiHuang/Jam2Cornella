using UnityEngine;
using UnityEngine.UI;

public class Credit: MonoBehaviour
{
    public float scrollSpeed = 50f;
    public RectTransform creditsRect;

    void Update()
    {
        if (creditsRect != null)
        {
            creditsRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        }
    }
}
