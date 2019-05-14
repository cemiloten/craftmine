using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInfo : IEquatable<TouchInfo> {
    private Vector2 _startPosition;
#if UNITY_EDITOR
    private int _fingerId;
    private Vector2 _position;

    public int FingerId => _fingerId;
    public Vector2 Position { get => _startPosition; set => _startPosition = value; }
    public Vector2 StartPosition => _startPosition;

    public TouchInfo(int fingerId, Vector2 position) {
        _fingerId = fingerId;
        _position = position;
        _startPosition = position;
    }

    public void UpdateInfo(Vector2 position) {
        _position = position;
    }

    public bool Equals(TouchInfo other) {
        return other == null ? false : other._fingerId == _fingerId;
    }

#else
    private Touch touch;

    public int FingerId => touch.fingerId;
    public TouchPhase Phase => touch.phase;
    public Vector2 Position => touch.position;
    public Vector2 StartPosition => _startPosition;

    public TouchInfo(Touch touch) {
        this.touch = touch;
        _startPosition = touch.position;
    }

    public void UpdateInfo(Touch touch) {
        this.touch = touch;
    }

    public bool Equals(TouchInfo other) {
        return other == null ? false : other.touch.fingerId == touch.fingerId;
    }
#endif

    public Vector2 Swipe => (_position - _startPosition);

    public override bool Equals(object o) {
        if (o == null)
            return false;
        return o as TouchInfo == null ? false : Equals(o);
    }

    public override int GetHashCode() { return base.GetHashCode(); }

    public static bool operator ==(TouchInfo t1, TouchInfo t2) {
        if ((object)t1 == null || (object)t2 == null)
            return object.Equals(t1, t2);
        return t1.Equals(t2);
    }

    public static bool operator !=(TouchInfo t1, TouchInfo t2) {
        if ((object)t1 == null || (object)t2 == null)
            return !object.Equals(t1, t2);
        return !t1.Equals(t2);
    }
}

public static class TouchManager {
    public delegate void OnTouchCountChangedHandler();
    public delegate void OnTouchStartHandler(TouchInfo touchInfo);
    public delegate void OnTouchHoldHandler(TouchInfo touchInfo);
    public delegate void OnTouchMoveHandler(TouchInfo touchInfo);
    public delegate void OnTouchEndHandler(TouchInfo touchInfo);
    public delegate void OnTouchCancelHandler(TouchInfo touchInfo);
    public static event OnTouchCountChangedHandler OnTouchCountChanged;
    public static event OnTouchStartHandler OnTouchStart;
    public static event OnTouchHoldHandler OnTouchHold;
    public static event OnTouchMoveHandler OnTouchMove;
    public static event OnTouchEndHandler OnTouchEnd;
    public static event OnTouchCancelHandler OnTouchCancel;

    private static List<TouchInfo> touchInfos = new List<TouchInfo>();

    public static int TouchCount { get; private set; }

    private static void UpdateMouseButton(int num) {
        if (Input.GetMouseButtonDown(num)) {
            TouchInfo touchInfo = new TouchInfo(num, Input.mousePosition);
            touchInfos.Add(touchInfo);
            OnTouchStart?.Invoke(touchInfo);
        }
        if (Input.GetMouseButtonUp(num)) {
            foreach (TouchInfo touchInfo in touchInfos) {
                if (touchInfo.FingerId == num) {
                    touchInfos.Remove(touchInfo);
                    OnTouchEnd?.Invoke(touchInfo);
                    break;
                }
            }
        }
        for (int i = 0; i < touchInfos.Count; ++i) {
            touchInfos[i].Position = Input.mousePosition;
        }
    }

    public static void Update() {
#if UNITY_EDITOR
        for (int i = 0; i < 3; ++i) {
            UpdateMouseButton(i);
        }
#else
        if (Input.touchCount != TouchCount && OnTouchCountChanged != null) {
            TouchCount = Input.touchCount;
            OnTouchCountChanged();
        }

        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; ++i) {
                Touch currentTouch = Input.touches[i];
                TouchInfo touchInfo = GetTouchInfo(currentTouch);

                if (touchInfo == null) {
                    touchInfos.Add(new TouchInfo(currentTouch));
                } else {
                    touchInfo.UpdateInfo(currentTouch);
                }

                switch (currentTouch.phase) {
                    case TouchPhase.Began:
                        OnTouchStart?.Invoke(touchInfo);
                        break;
                    case TouchPhase.Stationary:
                        OnTouchHold?.Invoke(touchInfo);
                        break;
                    case TouchPhase.Moved:
                        OnTouchMove?.Invoke(touchInfo);
                        break;
                    case TouchPhase.Ended:
                        touchInfos.Remove(touchInfo);
                        OnTouchEnd?.Invoke(touchInfo);
                        break;
                    case TouchPhase.Canceled:
                        touchInfos.Remove(touchInfo);
                        OnTouchCancel?.Invoke(touchInfo);
                        break;
                    default:
                        continue;
                }
            }
        }
#endif
    }

    private static TouchInfo GetTouchInfo(Touch touch) {
        for (int i = 0; i < touchInfos.Count; ++i) {
            if (touchInfos[i].FingerId == touch.fingerId) {
                return touchInfos[i];
            }
        }
        return null;
    }

}
