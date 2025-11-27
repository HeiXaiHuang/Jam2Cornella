using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour
{
    public Text textoMensaje;

    public void MostrarMensaje(int mensajeID)
    {
        if (mensajeID == 0)
            textoMensaje.text = "Porque no me constestas?";
        else if (mensajeID == 1)
            textoMensaje.text = "¿Te has enfadado?";
        else if (mensajeID == 2)
            textoMensaje.text = "Wayuebdyewbfh";
        else
            textoMensaje.text = "";
    }
}

