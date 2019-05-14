using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public GameObject blockPrefab;
    public int right;
    public int forward;
    public int maxHeight;
    private List<Block> blocks = new List<Block>();

    void Start() {
        GenerateMap();
    }

    void Update() { }

    void GenerateMap() {
        for (int x = 0; x < right; ++x) {
            for (int z = 0; z < forward; ++z) {
                int max = Random.Range(1, maxHeight);
                for (int y = 0; y < max; ++y) {
                    Block.Create(new Vector3Int(x, y, z), blockPrefab);
                }
            }
        }
    }
}
