using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int rows = 6;
    public int cols = 6;
    public float tileSize = 1f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector3 startPos = transform.position;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 spawnPos = new Vector3(
                    startPos.x + col * tileSize,
                    startPos.y - row * tileSize, // Go down for each row
                    0f);

                GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);

                // Alternate color based on checkerboard pattern
                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                if ((row + col) % 2 == 0)
                {
                    sr.color = new Color(0.9f, 0.7f, 0.5f); // Light color
                }
                else
                {
                    sr.color = new Color(0.7f, 0.4f, 0.2f); // Dark color
                }

                tile.name = $"Tile_{row}_{col}";
            }
        }
    }
}
