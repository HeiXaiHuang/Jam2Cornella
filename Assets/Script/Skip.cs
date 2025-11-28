using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip: MonoBehaviour
{
    public GameObject objetoActivar; 
    public GameObject finalActivar;
    public GameObject objetoDesactivar;
    public GameObject objetoDesactivar2;

    void Update()
    {
        // Detecta si se presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            objetoActivar.SetActive(true);    // Activa el primero
            objetoDesactivar.SetActive(false); // Desactiva el segundo
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetoActivar.SetActive(true);
            objetoDesactivar.SetActive(false);
            objetoDesactivar2.SetActive(false);

            // Inicia la corutina para esperar 3 segundos
            StartCoroutine(ActivarFinal());
        }
    }

    private System.Collections.IEnumerator ActivarFinal()
    {
        yield return new WaitForSeconds(7f); // espera 3 segundos
        finalActivar.SetActive(true);
    }
}
