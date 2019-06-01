using UnityEngine;

public enum ActionType {
    None = 0,
    Movement
}

public abstract class Action : MonoBehaviour {
    public float duration = 1f;

    private float _currentTime;
    private Cell _source;
    private Cell _target;

    private bool IsActive { get; set; }

    public ActionType Type { get; protected set; }

    protected abstract void SetActionType();

    protected abstract void UpdateAction(Cell source, Cell target, float currentTime);

    public delegate void OnActionStartHandler();

    public delegate void OnActionEndHandler();

    public event OnActionStartHandler OnActionStart;
    public event OnActionEndHandler OnActionEnd;

    public bool Act(Cell source, Cell target) {
        if (IsActive) {
            return false;
        }

        _source = source;
        _target = target;
        StartAction();
        return true;
    }

    private void StartAction() {
        IsActive = true;
        _currentTime = 0f;
        OnActionStart?.Invoke();
    }

    private void EndAction() {
        IsActive = false;
        OnActionEnd?.Invoke();
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

        // IsActive can become false in the call to EndAction that just happened,
        // so we need to check again to make sure that we still need to update.
        if (IsActive) {
            UpdateAction(_source, _target, _currentTime);
        }
    }
}