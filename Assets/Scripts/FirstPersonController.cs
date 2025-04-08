using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
  public bool CanMove { get; private set; } = true;

  [Header("Look Parameters")]
  [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
  [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;

  [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
  [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

  private Camera playerCamera;
  private CharacterController charController;

  private Vector3 moveDirection;
  private Vector2 currentInput;

  private float rotationX = 0;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    playerCamera = GetComponentInChildren<Camera>();
    charController = GetComponent<CharacterController>();
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (CanMove)
    {
      HandleMouseLook();
    }
  }

  private void HandleMouseLook()
  {
    rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
    rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
    playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
  }
}
