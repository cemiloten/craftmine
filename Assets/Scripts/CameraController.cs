using UnityEngine;

public class CameraController : MonoBehaviour {
    private Camera _camera;

    private void Start() {
        _camera = Camera.main;
        float height = MapManager.Instance.height;
        if (_camera != null) {
            _camera.transform.position = new Vector3(
                MapManager.Instance.width * 0.5f,
                height * 0.75f,
                -height * 0.8f);
        }
    }
}