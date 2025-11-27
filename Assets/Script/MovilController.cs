using UnityEngine;
using System.Collections;

public class MovilController : MonoBehaviour
{
    [Header("UI Móvil")]
    public RectTransform rect;
    public Vector2 posicionAbierta = Vector2.zero;
    public Vector2 posicionCerrada = new Vector2(500, -400);
    public Vector3 escalaAbierta = Vector3.one;
    public Vector3 escalaCerrada = new Vector3(0.6f, 0.6f, 1f);
    public float duracion = 0.35f;

    [Header("Pantallas")]
    public GameObject home;
    public GameObject chat;

    [Header("Notificación")]
    public NotificacionMovil notificacion;

    public bool Abierto { get; private set; } = false;
    private bool animando = false;

    private System.Action callback;

    void Start()
    {
        rect.anchoredPosition = posicionCerrada;
        rect.localScale = escalaCerrada;
        MostrarHome();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !animando)
        {
            StartCoroutine(AnimarMovil(callback));
        }
    }

    public void AbrirMovil(System.Action callbackAlAbrir)
    {
        callback = callbackAlAbrir;
        if (!animando)
            StartCoroutine(AnimarMovil(callback));
    }

    IEnumerator AnimarMovil(System.Action callback = null)
    {
        animando = true;

        Vector2 inicioPos = rect.anchoredPosition;
        Vector2 finPos = Abierto ? posicionCerrada : posicionAbierta;
        Vector3 inicioEscala = rect.localScale;
        Vector3 finEscala = Abierto ? escalaCerrada : escalaAbierta;

        float t = 0f;
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
            MostrarHome();
        }

        this.callback = null;
    }

    void MostrarHome()
    {
        home.SetActive(true);
        chat.SetActive(false);
    }
}
