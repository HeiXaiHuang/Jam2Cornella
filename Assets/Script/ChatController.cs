using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ChatController : MonoBehaviour
{
    public TMP_Text textoMensaje;
    public Button[] botonesOpciones;

    private Action<int> callbackRespuesta;

    public void MostrarMensajeNovia(string mensaje, bool mostrarOpciones, Action<int> callback = null)
    {
        textoMensaje.text = mensaje;
        callbackRespuesta = callback;

        foreach (var btn in botonesOpciones)
            btn.gameObject.SetActive(mostrarOpciones);

        if (mostrarOpciones)
        {
            for (int i = 0; i < botonesOpciones.Length; i++)
            {
                int index = i; // captura correcta
                botonesOpciones[i].onClick.RemoveAllListeners();
                botonesOpciones[i].onClick.AddListener(() => Responder(index));
            }
        }
    }

    void Responder(int opcion)
    {
        foreach (var btn in botonesOpciones)
            btn.gameObject.SetActive(false);

        callbackRespuesta?.Invoke(opcion);
    }
}
