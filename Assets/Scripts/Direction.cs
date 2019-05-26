using UnityEngine;

public enum Direction {
    None = 0,
    Up,
    Down,
    Right,
    Left,
}

public static class DirectionMethods {

    public static Direction FromVector(Vector2 vec) {
        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y)) {
            if (vec.x > 0f) {
                return Direction.Right;
            } else {
                return Direction.Left;
            }
        } else {
            if (vec.y > 0f) {
                return Direction.Up;
            } else {
                return Direction.Down;
            }
        }
    }
}
