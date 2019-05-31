using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager> {
    private readonly List<Cell> _cells = new List<Cell>();

    public GameObject blockPrefab;
    public int height;
    public int width;

    private void Start() {
        GenerateBlocks();
    }

    public Cell CellAt(Vector2Int position) {
        return _cells[position.x + position.y * width];
    }

    private void GenerateBlocks() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                GameObject go = Instantiate(blockPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                var cell = go.AddComponent<Cell>();
                cell.position = new Vector2Int(x, y);
                cell.type = CellType.Ground;
                _cells.Add(cell);
            }
        }
    }

    public bool IsPositionOnGrid(Vector2Int position) {
        return position.x >= 0 && position.x < width
                               && position.y >= 0 && position.y < height;
    }

    public static Vector3 ToWorldPosition(Vector2Int position) {
        return new Vector3(position.x + 0.5f, 0f, position.y + 0.5f);
    }
}