using UnityEngine;

public enum ActionType {
    None = 0,
    Movement,
}

public abstract class Action : MonoBehaviour {
    public float duration = 1f;

    private float _currentTime;
    private Cell _source;
    private Cell _target;

    private bool IsActive { get; set; }

    public ActionType Type { get; protected set; }

    protected abstract void SetActionType();
    protected abstract void OnActionStart();
    protected abstract void OnActionEnd();
    protected abstract void UpdateAction(Cell source, Cell target, float currentTime);

    public void Act(Cell source, Cell target) {
        Debug.Log("acting");
        _source = source;
        _target = target;
        StartAction();
    }

    private void StartAction() {
        IsActive = true;
        _currentTime = 0f;

        OnActionStart();
    }

    private void EndAction() {
        IsActive = false;

        OnActionEnd();
    }

    private void Awake() {
        SetActionType();
    }

    private void Update() {
        if (!IsActive) {
            return;
        }

        _currentTime += Time.deltaTime;
        if (_currentTime > duration) {
            EndAction();
        }
        UpdateAction(_source, _target, _currentTime);
    }
}