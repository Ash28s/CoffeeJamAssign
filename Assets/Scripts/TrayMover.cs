using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrayMover : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector3 dragOffset;

    [SerializeField] private float gridSize = 1f;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void OnMouseDown()
    {
        isDragging = true;
        dragOffset = transform.position - GetMouseWorldPos();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.None; // unlock movement
    }

    void OnMouseUp()
    {
        isDragging = false;
        SnapToGrid(transform);
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // freeze after placing
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector2 targetPos = GetMouseWorldPos() + dragOffset;
            Vector2 moveDir = (targetPos - (Vector2)transform.position);
            rb.velocity = moveDir * 10f;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }

    public void SwapWith(Transform otherTray)
    {
        Vector3 temp = transform.position;
        transform.position = otherTray.position;
        otherTray.position = temp;

        SnapToGrid(transform);
        SnapToGrid(otherTray);

        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        Rigidbody2D otherRB = otherTray.GetComponent<Rigidbody2D>();
        if (otherRB != null)
        {
            otherRB.velocity = Vector2.zero;
            otherRB.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void SnapToGrid(Transform obj)
    {
        Vector3 pos = obj.position;
        float x = Mathf.Round(pos.x / gridSize) * gridSize;
        float y = Mathf.Round(pos.y / gridSize) * gridSize;
        obj.position = new Vector3(x, y, pos.z);
    }
}
