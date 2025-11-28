using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a cargar

    // M¨¦todo normal: se puede llamar desde un bot¨®n o evento
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // M¨¦todo autom¨¢tico: se ejecuta al entrar un objeto con tag "Player" en el trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
