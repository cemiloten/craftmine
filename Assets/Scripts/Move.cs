using UnityEngine;

public class Move : Action {
    public float movementDuration = 0.25f;
    private float _movementTimer;

    protected override void SetActionType() {
        Type = ActionType.Movement;
    }

    protected override void UpdateAction() {
        throw new System.NotImplementedException();
    }
}