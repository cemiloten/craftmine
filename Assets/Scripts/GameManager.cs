using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager> {
    public GameObject playerPrefab;
    public int level = 0;

    private Player Player { get; set; }
    private PositionPair LevelBounds { get; set; }

    private void Start() {
        MapManager.Instance.GenerateBlocks();
        LevelBounds = MapManager.Instance.GenerateEntryAndExit();
        Cell cell = MapManager.Instance.CellAt(LevelBounds.End);
        cell.type = CellType.Portal;
        cell.transform.Translate(0f, 1f, 0f);

        PreparePlayer(LevelBounds.Start);
    }

    private void PreparePlayer(Vector2Int startPosition) {
        GameObject go = Instantiate(playerPrefab, MapManager.ToWorldPosition(startPosition), Quaternion.identity);
        Player = go.GetOrAddComponent<Player>() as Player;
        if (Player == null) {
            return;
        }

        Player.Move = go.GetOrAddComponent<Move>() as Move;
        if (Player.Move != null) {
            Player.Move.OnActionEnd += OnMoveEnd;
        }
    }

    private void Update() {
        TouchManager.Update();
        if (Input.GetKeyDown(KeyCode.Space)) {
            PositionPair pair = MapManager.Instance.GenerateEntryAndExit();
            Player.transform.position = MapManager.ToWorldPosition(pair.Start);
        }
    }

    private void OnMoveEnd() {
        Debug.Log("moved");
        Vector2Int pos = Player.Position;
        if (MapManager.Instance.IsPositionOnGrid(pos) && MapManager.Instance.CellAt(pos).type == CellType.Portal) {
            Debug.Log("is a win");
        }
    }
}