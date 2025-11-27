using UnityEngine;

public class TagTrigger : MonoBehaviour
{
    public string targetTag;
    public GameObject targetObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
            targetObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
            targetObject.SetActive(false);
    }
}
