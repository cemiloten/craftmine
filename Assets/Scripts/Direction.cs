using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public enum Direction {
    None = 0,
    Up,
    Down,
    Right,
    Left
}


public static class DirectionMethods {
    public static Direction FromVector(Vector2 vec) {
        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y)) {
            return vec.x > 0f ? Direction.Right : Direction.Left;
        }

        return vec.y > 0f ? Direction.Up : Direction.Down;
    }

    public static Vector2Int ToVector2Int(this Direction direction) {
        switch (direction) {
            case Direction.Up:    return Vector2Int.up;
            case Direction.Down:  return Vector2Int.down;
            case Direction.Right: return Vector2Int.right;
            case Direction.Left:  return Vector2Int.left;
            default:              return new Vector2Int(0, 0);
        }
    }
}