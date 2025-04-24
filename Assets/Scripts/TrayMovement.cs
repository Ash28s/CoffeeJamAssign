using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrayMovement : MonoBehaviour
{
    public float slideForce = 10f;
    private Rigidbody2D rb;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow)) direction = Vector2.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) direction = Vector2.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) direction = Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) direction = Vector2.right;

        if (direction != Vector2.zero)
        {
            rb.velocity = Vector2.zero; // clear current motion
            rb.AddForce(direction * slideForce, ForceMode2D.Impulse);
        }
    }
}
