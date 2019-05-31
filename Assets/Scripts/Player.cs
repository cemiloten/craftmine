using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public Vector2Int Position { get; private set; }
    public Move Move { get; set; }

    private void OnEnable() {
        TouchManager.OnTouchEnd += OnTouchEnd;
    }

    private void OnDisable() {
        TouchManager.OnTouchEnd -= OnTouchEnd;
    }

    private void Update() {
        var direction = Direction.None;
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            direction = Direction.Right;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            direction = Direction.Up;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction = Direction.Left;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            direction = Direction.Down;
        }

        if (direction != Direction.None) {
            MoveTowards(direction);
        }
    }

    private void OnTouchEnd(TouchInfo touchInfo) {
        Direction swipeDirection = DirectionMethods.FromVector(touchInfo.Swipe);
        if (swipeDirection != Direction.None) {
            MoveTowards(swipeDirection);
        }
    }

    private void MoveTowards(Direction direction) {
        Vector2Int targetPosition = Position + direction.ToVector2Int();
        if (!MapManager.Instance.IsPositionOnGrid(targetPosition)) {
            return;
        }

        if (Move.Act(MapManager.Instance.CellAt(Position), MapManager.Instance.CellAt(targetPosition))) {
            Position = targetPosition;
        }
    }
}