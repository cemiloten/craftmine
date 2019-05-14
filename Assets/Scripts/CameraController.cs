using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float _rotationDuration = 1f;

    private float _currentRotationTime;
    private bool _isRotating = false;
    private Quaternion _sourceRotation = Quaternion.identity;
    private Quaternion _targetRotation = Quaternion.identity;

    private void OnEnable() {
        TouchManager.OnTouchEnd += OnTouchEnd;
    }

    private void Start() {
        _sourceRotation = transform.localRotation;
    }

    private void Update() {
        TouchManager.Update();

        if (_isRotating) {
            UpdateRotation();
        }
    }

    private void OnTouchEnd(TouchInfo touchInfo) {
        if (_isRotating) {
            return;
        }
        Vector2 swipe = touchInfo.Swipe;
        if (swipe.magnitude > 0.1f * Mathf.Min(Screen.width, Screen.height)) {
            StartRotating(DirectionMethods.FromVector(swipe));
        }
    }

    private void StartRotating(Direction direction) {
        _isRotating = true;
        _currentRotationTime = 0f;

        _sourceRotation = transform.localRotation;
        Vector3 rotation = transform.localRotation.eulerAngles;
        switch (direction) {
            case Direction.Up:
                rotation.x += 90f;
                break;
            case Direction.Down:
                rotation.x -= 90f;
                break;
            case Direction.Right:
                rotation.y -= 90f;
                break;
            case Direction.Left:
                rotation.y += 90f;
                break;
            default:
                break;
        }
        _targetRotation = Quaternion.Euler(rotation);
    }

    private void StartMovement(Direction dir) {
        _isRotating = true;
        _currentRotationTime = 0f;

    }

    //private void UpdateOrbit() {
    //    transform.RotateAround(Vector3.zero, Vector3.up, _)
    //}

    private bool ShouldEndRotation() {
        return _currentRotationTime >= _rotationDuration;
    }


    private void UpdateRotation() {
        if (ShouldEndRotation()) {
            EndRotation();
        }

        transform.localRotation = Quaternion.Lerp(_sourceRotation, _targetRotation, _currentRotationTime);
        _currentRotationTime += Time.deltaTime;
    }

    private void EndRotation() {
        _isRotating = false;
        _currentRotationTime = 0f;
        _sourceRotation = _targetRotation;
    }
}
