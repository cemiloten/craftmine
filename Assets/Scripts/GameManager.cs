using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager> {
    public GameObject playerPrefab;
    public int level = 0;

    private Player Player { get; set; }

    private void Start() {
        PreparePlayer();
        if (Player != null) {
            if (Player.Move != null) {
                Player.Move.OnActionEnd += OnMoveEnd;
            } else {
                Debug.Log("move is null");
            }
        } else {
            Debug.Log("player is null");
        }
    }

    private void PreparePlayer() {
        GameObject go = Instantiate(playerPrefab, MapManager.ToWorldPosition(new Vector2Int(0, 0)),
            Quaternion.identity);
        Player = go.GetOrAddComponent<Player>() as Player;
        if (Player != null) {
            Player.Move = go.GetOrAddComponent<Move>() as Move;
        }
    }

    private void Update() {
        TouchManager.Update();
    }

    private void OnMoveEnd() {
        Vector2Int pos = Player.Position;
        if (MapManager.Instance.IsPositionOnGrid(pos) && MapManager.Instance.CellAt(pos).type == CellType.Portal) {
            // win
        }
    }
}