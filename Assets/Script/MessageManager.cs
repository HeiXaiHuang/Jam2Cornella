using UnityEngine;
using System.Collections;

public class MessageManager : MonoBehaviour
{
    [Header("Referencias")]
    public ChatController chat;
    public MovilController movil;
    public anxiety anxietySystem;

    [Header("Mensajes y respuestas")]
    public string[] mensajesNovia = new string[]
    {
        "¿Por qué no me contestas?",
        "¿Te has enfadado?",
        "Vale… ya veo."
    };

    public string[,] respuestasNovia = new string[,]
    {
        { "Ah… siempre igual contigo.", "Bueno… vale 💔", "😊" },
        { "Eso me duele", "Ok…", "Gracias por decirlo" },
        { "……", "Bien.", "Vale." }
    };

    [Header("Estado")]
    public int mensajeActual = 0;
    public bool notificacionPendiente = false;
    private bool notificacionLanzada = false;

    private const float ANXIETY_TRIGGER = 70f;
    private const int TOTAL_CONVERSACIONES = 3;

    void Start()
    {
        StartCoroutine(ForzarNotificacionInicio());
    }

    void Update()
    {
        if (anxietySystem == null || movil == null || chat == null) return;
        if (mensajeActual >= TOTAL_CONVERSACIONES) return;

        float anxiety = anxietySystem.GetAnxiety();

        if (!notificacionLanzada && anxiety >= ANXIETY_TRIGGER)
        {
            LanzarMensaje();
            notificacionLanzada = true;
        }
    }

    IEnumerator ForzarNotificacionInicio()
    {
        yield return null;
        if (mensajeActual < TOTAL_CONVERSACIONES && !notificacionLanzada)
        {
            LanzarMensaje();
            Debug.Log("Notificación forzada al inicio");
        }
    }

    public void LanzarMensaje()
    {
        if (notificacionPendiente) return;

        notificacionPendiente = true;

        if (movil.notificacion != null)
            movil.notificacion.Mostrar();

        Debug.Log("Notificación activada");
    }

    public void MostrarChat()
    {
        if (!notificacionPendiente) return;

        notificacionPendiente = false;

        chat.MostrarMensajeNovia(
            mensajesNovia[mensajeActual],
            true,
            RespuestaJugador
        );

        if (movil.notificacion != null)
            movil.notificacion.Ocultar();
    }

    void RespuestaJugador(int opcion)
    {
        string respuesta = respuestasNovia[mensajeActual, opcion];
        chat.MostrarMensajeNovia(respuesta, false);

        mensajeActual++;

        anxietySystem.AddAnxiety(-40f);
    }
}
