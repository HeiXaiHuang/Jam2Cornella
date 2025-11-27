using UnityEngine;
using System.Collections;

public class MovilController : MonoBehaviour
{
    public RectTransform rect;
    public Vector2 posicionAbierta = Vector2.zero;
    public Vector2 posicionCerrada = new Vector2(500, -400);
    public Vector3 escalaAbierta = Vector3.one;
    public Vector3 escalaCerrada = new Vector3(0.6f, 0.6f, 1f);
    public float duracion = 0.35f;
    public GameObject home;
    public GameObject chat;
    public NotificacionMovil notificacion;

    public bool Abierto { get; private set; } = false;
    private bool animando = false;

void Update()
{
    if (Input.GetKeyDown(KeyCode.M) && !animando)
    {
        var mm = FindObjectOfType<MessageManager>();

        StartCoroutine(AnimarMovil(() =>
        {
            if (Abierto && mm.notificacionPendiente)
            {
                // Abrir chat
                mm.MostrarChat();

                // Ocultar la notificación
                notificacion.Ocultar();
            }
        }));
    }
}


    void Start()
    {
        rect.anchoredPosition = posicionCerrada;
        rect.localScale = escalaCerrada;
        MostrarHome();
    }

    public IEnumerator AnimarMovil(System.Action callback = null)
    {
        if (animando) yield break;

        animando = true;
        Vector2 inicioPos = rect.anchoredPosition;
        Vector2 finPos = Abierto ? posicionCerrada : posicionAbierta;
        Vector3 inicioEscala = rect.localScale;
        Vector3 finEscala = Abierto ? escalaCerrada : escalaAbierta;

        float t = 0;
        while (t < duracion)
        {
            t += Time.unscaledDeltaTime;
            float lerp = t / duracion;
            rect.anchoredPosition = Vector2.Lerp(inicioPos, finPos, lerp);
            rect.localScale = Vector3.Lerp(inicioEscala, finEscala, lerp);
            yield return null;
        }
        rect.anchoredPosition = finPos;
        rect.localScale = finEscala;

        Abierto = !Abierto;

        if (Abierto) MostrarHome();

        animando = false;

        callback?.Invoke();

        if (!Abierto)
{
    // SI SE CIERRA → volver siempre a Home
    home.SetActive(true);
    chat.SetActive(false);
}

    }

    void MostrarHome()
    {
        home.SetActive(true);
        chat.SetActive(false);
    }
}

