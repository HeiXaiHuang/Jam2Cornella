using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ChatController : MonoBehaviour
{
    public TMP_Text textoMensaje;      // Texto donde se muestra el mensaje
    public Button[] botonesOpciones;   // Los 3 botones de respuesta

    private Action<int> callbackRespuesta;

    // ---------------------------
    // Mostrar mensaje y botones
    // ---------------------------
    public void MostrarMensajeNovia(string mensaje, bool mostrarOpciones, Action<int> callback = null)
    {
        textoMensaje.text = mensaje;
        callbackRespuesta = callback;

        // Activar o desactivar botones
        foreach (var btn in botonesOpciones)
            btn.gameObject.SetActive(mostrarOpciones);

        if (mostrarOpciones)
        {
            // Asignar callback a cada botón
            for (int i = 0; i < botonesOpciones.Length; i++)
            {
                int index = i; // Capturar variable local
                botonesOpciones[i].onClick.RemoveAllListeners();
                botonesOpciones[i].onClick.AddListener(() => Responder(index));
            }
        }
    }

    void Responder(int opcion)
    {
        // Ocultar botones al responder
        foreach (var btn in botonesOpciones)
            btn.gameObject.SetActive(false);

        // Llamar callback
        callbackRespuesta?.Invoke(opcion);
    }
}
