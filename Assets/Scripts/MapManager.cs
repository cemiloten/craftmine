using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public GameObject blockPrefab;
    public int height;
    public int width;

    public static MapManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        GenerateBlocks();
    }

    private void Update() { }

    private void GenerateBlocks() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                GameObject go = Instantiate(blockPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                Cell cell = go.AddComponent<Cell>() as Cell;
                cell.Initialize(new Vector2Int(x, y), CellType.Ground);
            }
        }
    }

    public static Vector3 ToWorldPosition(Vector2Int position) {
        return new Vector3(position.x + 0.5f, 0f, position.y + 0.5f);
    }
}