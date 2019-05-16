using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float _rotationDuration = 1f;
    public float _rotationAmount = 90f;
    public float _distanceToTarget = 5f;
    public Vector3 target;

    private bool _isRotating = false;
    private float _step;
    private float _smoothStep;
    private float _lastStep;
    private float _direction = 1f;

    private Vector3 _axis;

    private float Rate => 1f / _rotationAmount;

    private void OnEnable() {
        TouchManager.OnTouchEnd += OnTouchEnd;
    }

    private void Start() {
        Vector3 pos = target;
        pos.z -= _distanceToTarget;
        transform.position = pos;
    }

    private void Update() {
        TouchManager.Update();

        if (_isRotating) {
            UpdateRotation();
        }
    }

    private void OnTouchEnd(TouchInfo touchInfo) {
        if (_isRotating) {
            // Don't accept new input if not done.
            return;
        }
        Vector2 swipe = touchInfo.Swipe;
        if (swipe.magnitude > 0.1f * Mathf.Min(Screen.width, Screen.height)) {
            StartRotating(DirectionMethods.FromVector(swipe));
        }
    }

    private void StartRotating(Direction direction) {
        _isRotating = true;
        _step = 0f;
        _smoothStep = 0f;
        _lastStep = 0f;

        switch (direction) {
            case Direction.Up:
                _axis = Vector3.Cross(transform.up.normalized, (target - transform.position).normalized);
                _direction = 1f;
                break;
            case Direction.Down:
                _axis = Vector3.Cross(transform.up.normalized, (target - transform.position).normalized);
                _direction = -1f;
                break;
            case Direction.Right:
                _axis = Vector3.Cross((target - transform.position).normalized, transform.right.normalized);
                _direction = -1f;
                break;
            case Direction.Left:
                _axis = Vector3.Cross((target - transform.position).normalized, transform.right.normalized);
                _direction = 1f;
                break;
            default:
                break;
        }
    }

    private bool ShouldEndRotation() => _step > 1f;

    private void UpdateRotation() {

        _step += Time.deltaTime * Rate;
        _smoothStep = Mathf.SmoothStep(0f, 1f, _step);

        transform.RotateAround(target, _axis, _direction * (_rotationAmount * (_smoothStep - _lastStep)));
        _lastStep = _smoothStep;

        if (ShouldEndRotation()) {
            EndRotation();
        }
    }

    private void EndRotation() {
        transform.RotateAround(target, _axis, _rotationAmount * (1f - _lastStep));
        _isRotating = false;
    }
}
