using UnityEngine;

public class MouseLookNewInput : MonoBehaviour {
    public float mouseSensitivity = 100f;
    private Vector2 lookInput;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private PlayerControls controls;
    private Vector3 fixedPosition;

    void Awake() {
        controls = new PlayerControls();
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        Vector3 rot = transform.eulerAngles;
        yRotation = rot.y;
        xRotation = 30f;

        fixedPosition = transform.position;
    }

    void Update() {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.position = fixedPosition;
    }
}
