using System;
using UnityEngine;

public class Player : MonoBehaviour {
    private Move _move;

    public Vector2Int position;
    public Direction direction;
    public Action[] actions;


    private void Start() {
        actions = GetComponents<Action>();
        foreach (Action action in actions) {
            if (action.Type != ActionType.Movement)
                continue;

            _move = action as Move;
            break;
        }

        if (_move == null) {
            throw new Exception("move is null");
        }
    }

    private void OnEnable() {
        TouchManager.OnTouchEnd += OnTouchEnd;
    }

    private void OnDisable() {
        TouchManager.OnTouchEnd -= OnTouchEnd;
    }

    private void OnTouchEnd(TouchInfo touchInfo) {
        Cell source = MapManager.Instance.CellAt(position);

        Direction swipeDirection = DirectionMethods.FromVector(touchInfo.Swipe);
        Cell target =
            MapManager.Instance.CellAt(position + swipeDirection.ToVector2Int());

        _move.Act(source, target);
        position = target.position;
    }
}