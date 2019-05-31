using System;
using UnityEngine;

public class Move : Action {
    protected override void SetActionType() {
        Type = ActionType.Movement;
    }

    protected override void UpdateAction(Cell source, Cell target, float currentTime) {
        transform.position = Vector3.Lerp(
            MapManager.ToWorldPosition(source.position),
            MapManager.ToWorldPosition(target.position),
            currentTime / duration);
    }
}