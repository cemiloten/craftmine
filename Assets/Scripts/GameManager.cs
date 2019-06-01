using System;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public GameObject playerPrefab;
    public int level = 0;

    private Player Player { get; set; }
    private PositionPair LevelBounds { get; set; }

    private void OnDisable() {
        Player.Move.OnActionEnd -= OnMoveEnd;
    }

    private void Start() {
        GenerateFirstLevel();
    }

    private void Update() {
        TouchManager.Update();
        if (Input.GetKeyDown(KeyCode.Space)) {
            GenerateLevel();
        }
    }

    private void GenerateFirstLevel() {
        MapManager.Instance.InstantiateBlocks();
        Vector2Int startPosition = MapManager.Instance.GenerateStartPosition();
        InstantiatePlayer(startPosition);
        Vector2Int endPos = MapManager.Instance.GenerateEndPosition(startPosition);
        Cell cell = MapManager.Instance.CellAt(endPos);
        cell.type = CellType.Portal;
        cell.transform.Translate(0f, 1f, 0f);
    }

    private void GenerateLevel() {
        MapManager.Instance.ResetBlocks();
        Vector2Int startPosition = MapManager.Instance.GenerateStartPosition();
        Player.Position = startPosition;
        Player.transform.position = MapManager.ToWorldPosition(startPosition);
        
        Vector2Int endPos = MapManager.Instance.GenerateEndPosition(startPosition);
        Cell cell = MapManager.Instance.CellAt(endPos);
        cell.type = CellType.Portal;
        cell.transform.Translate(0f, 1f, 0f);
    }

    private void InstantiatePlayer(Vector2Int startPosition) {
        GameObject go = Instantiate(playerPrefab, MapManager.ToWorldPosition(startPosition), Quaternion.identity);
        Player = go.GetOrAddComponent<Player>() as Player;
        if (Player == null) {
            throw new NullReferenceException("Couldn't get or add Player Component");
        }

        Player.Position = startPosition;

        Player.Move = go.GetOrAddComponent<Move>() as Move;
        if (Player.Move == null) {
            throw new NullReferenceException("Couldn't get or add Move Component");
        }

        Player.Move.OnActionEnd += OnMoveEnd;
    }

    private void OnMoveEnd() {
        Vector2Int pos = Player.Position;
        if (!MapManager.Instance.IsPositionOnGrid(pos) || MapManager.Instance.CellAt(pos).type != CellType.Portal) {
            return;
        }

        ++level;
        GenerateLevel();
    }
}