using System;
using UnityEngine;

public class Player : MonoBehaviour {
    private Move _move;
    
    public Vector2Int position;
    public Direction direction;
    public Action[] actions;
    

    private void Awake() {
        actions = GetComponents<Action>();
        foreach (Action t in actions) {
            if (t.Type != ActionType.Movement) continue;
            _move = t as Move;
            break;
        }
    }

    private void OnEnable() {
        TouchManager.OnTouchEnd += OnTouchEnd;
    }

    private void OnTouchEnd(TouchInfo touchInfo) {
        Cell source = MapManager.Instance.CellAt(position);
        Cell target = MapManager.Instance.CellAt(position);
        _move.Act();
    }
}