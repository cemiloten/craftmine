using System;
using UnityEngine;

public class Move : Action {
    protected override void SetActionType() {
        Type = ActionType.Movement;
    }

    protected override void UpdateAction(Cell source, Cell target, float currentTime) {
        float y = transform.position.y;
        transform.position = Vector3.Lerp(MapManager.ToWorldPosition(source.position, y),
                                          MapManager.ToWorldPosition(target.position, y),
                                          currentTime / duration);
    }
}