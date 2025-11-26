using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float move_h = Input.GetAxis("Horizontal");
        animator.SetFloat("movement", move_h * speed);

        Vector3 move = new Vector3(move_h, 0f);
        transform.Translate(move * speed * Time.deltaTime);

        if (move_h < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (move_h > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
