using UnityEngine;

public class NotificacionMovil : MonoBehaviour
{
    public GameObject icono;

    void Start()
    {
        if (icono != null)
            icono.SetActive(false);
    }

    public void Mostrar()
    {
        if (icono != null)
            icono.SetActive(true);
    }

    public void Ocultar()
    {
        if (icono != null)
            icono.SetActive(false);
    }
}
