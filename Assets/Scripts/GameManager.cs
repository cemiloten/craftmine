using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject playerPrefab;

    private void Start() {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }
}