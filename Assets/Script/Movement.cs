using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(h, 0f);
        transform.Translate(move * speed * Time.deltaTime);
    }
}
