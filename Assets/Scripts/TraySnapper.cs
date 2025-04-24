using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class TraySnapper : MonoBehaviour
{
    public Tilemap tilemap;
    public float snapThreshold = 0.05f;
    public float gridSize = 1f;

    private Rigidbody2D rb;
    private bool hasSnapped = false;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (!hasSnapped && rb.velocity.magnitude < snapThreshold)
        {
            SnapToGrid(transform);
            hasSnapped = true;

            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll; // freeze to prevent drift
        }

        if (rb.velocity.magnitude > snapThreshold)
        {
            hasSnapped = false;
            rb.constraints = RigidbodyConstraints2D.None; // unfreeze for movement
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
