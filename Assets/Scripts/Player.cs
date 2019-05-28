using System;
using UnityEngine;

public class Player : MonoBehaviour {
    private Move _move;

    public Vector2Int position;
    public Direction direction;
    public Action[] actions;


    private void Awake() {
        actions = GetComponents<Action>();
        foreach (Action action in actions) {
            if (action.Type != ActionType.Movement) continue;
            _move = action as Move;
            break;
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
        
        Direction dir = DirectionMethods.FromVector(touchInfo.Swipe);
        Cell target = MapManager.Instance.CellAt(dir.AddDirection(position));
        
        _move.Act(source, target);
        Debug.Log("on touchend");
    }
}