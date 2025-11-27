using UnityEngine;

public class NotificacionMovil : MonoBehaviour
{
    public GameObject icono;

    void Start()
    {
        icono.SetActive(false);
    }

    public void Mostrar()
    {
        icono.SetActive(true);
    }

    public void Ocultar()
    {
        icono.SetActive(false);
    }
}
