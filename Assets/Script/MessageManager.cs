using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public int mensajeActual = 0;
    public int totalMensajes = 3;
    public ChatController chat;


    public NotificacionMovil notificacion;

    void Update()
    {
        /// CÓDIGO DE PRUEBA
        if (Input.GetKeyDown(KeyCode.P))
        {
            LanzarMensaje();
        }
    }

    public void LanzarMensaje()
    {
        if (mensajeActual >= totalMensajes)
            return;

        Debug.Log("Llega mensaje " + (mensajeActual + 1));

        notificacion.Mostrar();

        mensajeActual++;
    }
}
