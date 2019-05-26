using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Vector2Int position;
    public Direction direction;
    public Action[] actions;

    private void Awake() {
        actions = GetComponents<Action>();
    }
}