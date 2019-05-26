using UnityEngine;

public enum ActionType {
    None = 0,
    Movement,
}

public abstract class Action : MonoBehaviour {
    protected Cell _source;
    protected Cell _target;

    public bool IsActive { get; protected set; }
    public ActionType Type { get; protected set; }

    protected abstract void SetActionType();
    
    protected abstract void UpdateAction();

    protected delegate void OnActionStartDelegate(Cell source, Cell target);

    public void Act() {
        OnActionStart();
    }

    protected virtual void OnActionStart() {
        IsActive = true;
    }

    protected virtual void OnActionEnd() {
        IsActive = false;
    }


    private void Awake() {
        SetActionType();

    }

    private void Update() {
        if (IsActive) {
            UpdateAction();
        }
    }
}
