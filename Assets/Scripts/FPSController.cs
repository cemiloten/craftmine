using UnityEngine;

public class FPSController : MonoBehaviour {
    public Camera cam;
    [Range(0.1f, 100f)]
    public float speed = 1f;


    private void Start() {
        cam.transform.parent = transform;
        cam.transform.position = transform.position;
    }

    private void Update() {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 translation = new Vector3(h, 0f, v) * speed * Time.deltaTime;

        if (v < 0f || v > 0f || h < 0f || h > 0f) {
            transform.Translate(translation);
        }

    }
}
