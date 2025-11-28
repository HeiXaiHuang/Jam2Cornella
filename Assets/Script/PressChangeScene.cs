using UnityEngine;
using UnityEngine.SceneManagement;

public class PressChangeScene : MonoBehaviour
{
    public string sceneName;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
