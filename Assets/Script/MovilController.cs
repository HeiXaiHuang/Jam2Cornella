using UnityEngine;
using System.Collections;

public class MovilController : MonoBehaviour
{
    RectTransform rect;

    [Header("Posiciones")]
    public Vector2 posicionAbierta = Vector2.zero;
    public Vector2 posicionCerrada = new Vector2(500, -400);
    public NotificacionMovil notificacion;

    [Header("Escala")]
    public Vector3 escalaAbierta = Vector3.one;
    public Vector3 escalaCerrada = new Vector3(0.6f, 0.6f, 1f);

    [Header("Animación")]
    public float duracion = 0.35f;

    [Header("Pantallas")]
    public GameObject home;
    public GameObject chat;

    bool abierto = false;
    bool animando = false;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

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
        StartCoroutine(AnimarMovil());
    }

    // TEST: simular mensaje entrante
    if (Input.GetKeyDown(KeyCode.N))
    {
        notificacion.Mostrar();
    }
    }


    IEnumerator AnimarMovil()
    {
        animando = true;

        Vector2 posInicio = rect.anchoredPosition;
        Vector2 posFinal = abierto ? posicionCerrada : posicionAbierta;

        Vector3 escalaInicio = rect.localScale;
        Vector3 escalaFinal = abierto ? escalaCerrada : escalaAbierta;

        float t = 0f;

        while (t < duracion)
        {
            t += Time.unscaledDeltaTime;
            float lerp = t / duracion;

            rect.anchoredPosition = Vector2.Lerp(posInicio, posFinal, lerp);
            rect.localScale = Vector3.Lerp(escalaInicio, escalaFinal, lerp);

            yield return null;
        }

        rect.anchoredPosition = posFinal;
        rect.localScale = escalaFinal;

        if (!abierto)
        {
            notificacion.Ocultar();
        }

        if (abierto)
        {
            MostrarHome();
        }

        abierto = !abierto;
        animando = false;
    }

    void MostrarHome()
    {
        home.SetActive(true);
        chat.SetActive(false);
    }
}
