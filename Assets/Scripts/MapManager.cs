using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public GameObject blockPrefab;
    public int size;

    private List<Block> blocks = new List<Block>();

    public static MapManager Instance { get; private set; }
    public static int Size => Instance.size;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        GenerateMap();
    }

    void Update() { }

    void GenerateMap() {
        for (int x = 0; x < size; ++x) {
            for (int z = 0; z < size; ++z) {
                for (int y = 0; y < size; ++y) {
                    if (Random.Range(0, 2) > 0) {
                        Block.Create(new Vector3Int(x, y, z), blockPrefab);
                    }
                }
            }
        }
    }

    public static Vector3 GetWorldPos(Vector3Int position) {
        int size = Size;
        return new Vector3(
            position.x - size / 2f + 0.5f,
            position.y - size / 2f + 0.5f,
            position.z - size / 2f + 0.5f);
    }

}
