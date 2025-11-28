using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public ChatController chat;
    public GameObject notificacion;

    int conversacionActual = 0;

    string[] mensajes =
    {
        "You know what's ansiety? Is just cause I'm suffering that right know: My parents are fighting and its not the first time that happens, you know what I mean? I'm very worried babe, hellllllpppppp meee"
        ,
        "I'm tidying up my bedrrom love<3. Could you do it for me jejejejeje? Cause I'm too lazy and I don't wanna spend hours and hours ",
        "Eyeyeyyeyeeyeyeye, right now I'm listening 'Me falta el Aliento' from Estopa. I absolutely recommend you to listen it at moments when u feel ver nervous. In my case, it relaxes me a lot babe"
    };

    string[][] opciones =
    {
        new string[]{"Whyyyyy, what happened??????", "Maybe is something normal, they're humans and they have emotions. I wish it wouldn't be anything bad", "But,but, but whaaaattt??? You're suffering smt similar as me"},
        new string[]{"I'm working out at the gym. I don't have enough time cause I have many tasks", "I'm very busy recently. Life is unfair with me. WHY GOD IS DEALING WITH ME LIKE THAT?", "I cannot, I've lots of work to do and I have to help u? Ah yes, if you pay me, I'd be very grateful in helping u <3"},
        new string[]{"Bro, I'm tidying up my bedroom and I really like to listen it, however I don't have enough storage in my phone in order to install Spotify", "I don't really like it sincerely, it is very old", "I don't feel well, we can we meet plsssss"}
    };

    string[][] respuestas =
    {
        new string[]{ "I've said it to you. MY PARENTS were coming to blows. You don't see that I couldn't do it, I DON'T WANT THAT HAPPENING AGAIN BABE!!!!!", "Which tasks are more important than your girlfriend???? You DON'T LOVE ME, ALL GUYS ARE THE SAME!!!!", "You're lying, I want to see you at the PARK right now!!!" },
        new string[]{ "ALL BOYS ARE THE SAME!!! Why don't you understand me??? You couldn't say me anything better? F***ING SELFISH? Are you thinking that you're the best one? Give it a sh*t", "Cause you don't believe me, you don't listen to me babe, but what happened to you actually?", "All guys are the same. Could you say smt happier, OK?? Come to the park come on!" },
        new string[]{ "HOOOOWWWWW??!!!!!! How many chances do we have to suffer the same situation?? I just wanna hear your version", "JAJJAJAJAAJ, you're so funny, but thank u so much. You could go to that door named 'SH*T' and don't be fucking asshole" , "Yeah sure, we meet at 3 pm at the Park, you know where no?" }
    };

    bool notificacionPendiente = false;

    // ---------- BOTÓN TEST ----------
    public void ForzarNotificacion()
    {
        if (conversacionActual >= 3) return;

        notificacion.SetActive(true);
        notificacionPendiente = true;

        Debug.Log("Notificación activada");
    }

    void Start()
    {
        ForzarNotificacion();
    }

    public void AbrirChatSiHayNotificacion()
    {
        if (!notificacionPendiente) return;

        notificacion.SetActive(false);
        notificacionPendiente = false;

        chat.MostrarMensaje(
            mensajes[conversacionActual],
            opciones[conversacionActual],
            Responder
        );
    }

    void Responder(int opcion)
    {
        chat.MostrarRespuesta(respuestas[conversacionActual][opcion]);
        conversacionActual++;
    }
}
