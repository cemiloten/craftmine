using System.Collections.Generic;
using UnityEngine;

public struct PositionPair {
    public Vector2Int Start;
    public Vector2Int End;
}

public class MapManager : Singleton<MapManager> {
    private readonly List<Cell> _cells = new List<Cell>();

    public GameObject blockPrefab;
    public int height = 10;
    public int width = 10;
    public int startOffset = 2;
    public int minDistanceToGoal = 8;

    public Cell CellAt(Vector2Int position) {
        return !IsPositionOnGrid(position) ? null : _cells[position.x + position.y * width];
    }

    public void InstantiateBlocks() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                GameObject go = Instantiate(blockPrefab, ToWorldPosition(new Vector2Int(x, y), -1f),
                                            Quaternion.identity);
                var cell = go.AddComponent<Cell>();
                cell.position = new Vector2Int(x, y);
                cell.type = CellType.Ground;
                _cells.Add(cell);
            }
        }
    }

    public void ResetBlocks() {
        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                var pos = new Vector2Int(x, y);
                Cell cell = CellAt(pos);
                cell.type = CellType.Ground;
                cell.transform.position = ToWorldPosition(pos, -1f);
            }
        }
    }

    public Vector2Int GenerateStartPosition() {
        var offset = new Vector2Int(Random.Range(0, startOffset), Random.Range(0, startOffset));
        return new Vector2Int(Random.Range(0, 2) > 0 ? width - 1 - offset.x : offset.x,
                              Random.Range(0, 2) > 0 ? height - 1 - offset.y : offset.y);
    }

    public Vector2Int GenerateEndPosition(Vector2Int startPosition) {
        return new Vector2Int(Random.Range(0, width),
                              Random.Range(0, height));
    }

    public bool IsPositionOnGrid(Vector2Int position) {
        return position.x > -1 && position.x < width && position.y > -1 && position.y < height;
    }

    public static Vector3 ToWorldPosition(Vector2Int position, float y = 0f) {
        return new Vector3(position.x + 0.5f, y, position.y + 0.5f);
    }
}