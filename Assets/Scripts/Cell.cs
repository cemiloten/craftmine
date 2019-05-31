using UnityEngine;

public enum CellType {
    None = 0,
    Ground,
    Portal
}

public class Cell : MonoBehaviour {
    public Vector2Int position;
    public CellType type;
}