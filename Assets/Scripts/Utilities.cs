using System;
using UnityEngine;

public static class Utilities { }

public static class UnityExtensions {
    public static Component GetOrAddComponent<T>(this GameObject gameObject)
        where T : Component {
        return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
    }
}