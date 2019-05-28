using System;
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

    public static Vector2Int AddDirection(this Direction direction, Vector2Int source) {
        switch (direction) {
            case Direction.Up:
                return new Vector2Int(source.x, source.y + 1);
                break;
            case Direction.Down:
                return new Vector2Int(source.x, source.y - 1);
                break;
            case Direction.Right:
                return new Vector2Int(source.x + 1, source.y);
                break;
            case Direction.Left:
                return new Vector2Int(source.x - 1, source.y);
                break;
            case Direction.None:
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}