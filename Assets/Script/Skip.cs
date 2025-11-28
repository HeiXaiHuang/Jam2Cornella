using UnityEngine;

public class Skip: MonoBehaviour
{
    public GameObject objetoActivar;   // El objeto que se activar¨¢
    public GameObject objetoDesactivar; // El objeto que se desactivar¨¢

    void Update()
    {
        // Detecta si se presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            objetoActivar.SetActive(true);    // Activa el primero
            objetoDesactivar.SetActive(false); // Desactiva el segundo
        }
    }
}
