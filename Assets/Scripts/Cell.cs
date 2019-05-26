using UnityEngine;

public enum CellType {
    None = 0,
    Ground,
}

public class Cell : MonoBehaviour {
    public Vector2Int position;
    public CellType type;

  public void Initialize(Vector2Int position, CellType type) {
        this.position = position;
        this.type = type;
    }
}