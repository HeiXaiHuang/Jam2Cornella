using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [Header("Referencias")]
    public ChatController chat;             // ChatController con TMP y botones
    public MovilController movil;           // MovilController que abre/cierra móvil

public anxiety anxietySystem;

private int conversacionActual = 0;
private const int TOTAL_CONVERSACIONES = 3;


    [Header("Mensajes")]
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
    public bool notificacionPendiente = false; // Solo abrir móvil si hay mensaje

    void Update()
    {
        // Protecciones
        if (anxietySystem == null) return;
        if (mensajeActual >= mensajesNovia.Length) return;
        
        // Si la ansiedad supera 80 y no hay notificación pendiente, lanzar mensaje
        if (!notificacionPendiente && (GetAnxiety() > 80.0f))
        {
            print("Anxiety: " + GetAnxiety());
            LanzarMensaje();
        }
    }

    // Wrapper para leer el valor desde el script 'anxiety'
    public float GetAnxiety()
    {
        if (anxietySystem == null) return 0f;
        return anxietySystem.GetAnxiety();
    }
    
    // ---------------------------
    // LLAMAR CUANDO LLEGA UN MENSAJE
    // ---------------------------
public void LanzarMensaje()
{
    print("Message started: " + GetAnxiety());
    //if (mensajeActual >= mensajesNovia.Length) return;

    // Mostrar notificación

    movil.notificacion.Mostrar();
    print("Cerida a la funció");
    // Marcar mensaje pendiente
    notificacionPendiente = true;
}


    // ---------------------------
    // LLAMAR DESDE MovilController CUANDO SE ABRE EL MÓVIL
    // ---------------------------
    public void MostrarChat()
    {
        if (!notificacionPendiente) return; // No abrir si no hay mensaje

        // Ya se abrió el chat → reset flag
        notificacionPendiente = false;

        // Mostrar mensaje actual con botones
        chat.MostrarMensajeNovia(
            mensajesNovia[mensajeActual],
            true,
            RespuestaJugador
        );
    }

    // ---------------------------
    // CALLBACK CUANDO EL JUGADOR RESPONDE
    // ---------------------------
    void RespuestaJugador(int opcion)
    {
        // Obtener respuesta de la novia
        string respuesta = respuestasNovia[mensajeActual, opcion];

        // Mostrar respuesta sin botones
        chat.MostrarMensajeNovia(respuesta, false);

        mensajeActual++;
    }
}
