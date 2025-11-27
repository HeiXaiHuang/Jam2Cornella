using UnityEngine;

public class HomeMovil : MonoBehaviour
{
    public GameObject chat;

    public void AbrirChat()
    {
        chat.SetActive(true);
        gameObject.SetActive(false);
    }
}
    