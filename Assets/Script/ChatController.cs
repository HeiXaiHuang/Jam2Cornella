using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ChatController : MonoBehaviour
{
    public TMP_Text textoMensaje;

    public Button[] botones;
    public TMP_Text[] textosBotones;

    private Action<int> callbackRespuesta;

    public void MostrarMensaje(string mensaje, string[] opciones, Action<int> callback)
    {
        textoMensaje.text = mensaje;
        callbackRespuesta = callback;

        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].gameObject.SetActive(true);
            textosBotones[i].text = opciones[i];

            int index = i;
            botones[i].onClick.RemoveAllListeners();
            botones[i].onClick.AddListener(() => PulsarOpcion(index));
        }
    }

    public void MostrarRespuesta(string texto)
    {
        textoMensaje.text = texto;

        foreach (var b in botones)
            b.gameObject.SetActive(false);
    }

    void PulsarOpcion(int index)
    {
        callbackRespuesta?.Invoke(index);
    }
}
