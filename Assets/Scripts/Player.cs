using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public Vector2Int Position { get; set; }
    public Move Move { get; set; }

    private void OnEnable() {
        TouchManager.OnTouchEnd += OnTouchEnd;
        Move.OnActionEnd += OnMoveEnd;
    }

    private void OnMoveEnd() {
        // todo: find a way to force the player's transform.position to the correct value.
        // think about a better way to implement action events...
        // Position = new position after move...
        // transform.position = new position into world position...
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

        if (Move.Act(MapManager.Instance.CellAt(Position),
                     MapManager.Instance.CellAt(targetPosition))) {
            Position = targetPosition;
        }
    }
}