using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private float velocityX = 0f;

    void LateUpdate()
    {
        float targetX = target.position.x;
        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocityX, smoothTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
