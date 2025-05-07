using UnityEngine;
using UnityEngine.InputSystem;

public class Fourth_RotateProtoboardStep : ITutorialStep {
    private bool isRotating = false; 
    private Vector2 lastMousePosition;
    private float rotationSpeed = 1.0f; 
    private Vector3 targetRotationEuler; 
    private Quaternion targetRotation; 
    private float dampingSpeed = 10f; 
    private Camera mainCamera;

    public void Enter(StepsManager manager){
        Debug.Log("4º Step Iniciado");
        manager.tutorialText.text = "Clique e arraste o mouse para rotacionar a Protoboard.";
        mainCamera = Camera.main;
        targetRotation = manager.protoboard.transform.rotation;
        targetRotationEuler = targetRotation.eulerAngles;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
    }

    public void Update(StepsManager manager){
        if (Mouse.current.leftButton.wasPressedThisFrame){
            isRotating = true;
            lastMousePosition = Mouse.current.position.ReadValue();
        }

        if (Mouse.current.leftButton.isPressed && isRotating){
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            Vector2 mouseDelta = currentMousePosition - lastMousePosition;

            float rotationX = -mouseDelta.y * rotationSpeed; 
            float rotationY = mouseDelta.x * rotationSpeed;  

            targetRotationEuler += new Vector3(rotationX, rotationY, 0);

            targetRotationEuler.x = Mathf.Clamp(targetRotationEuler.x, -90f, 90f);

            Vector3 cameraRight = mainCamera.transform.right;
            Vector3 cameraUp = mainCamera.transform.up;

            Quaternion rotX = Quaternion.AngleAxis(rotationX, cameraRight);
            Quaternion rotY = Quaternion.AngleAxis(rotationY, cameraUp);
            targetRotation = rotY * rotX * targetRotation;

            lastMousePosition = currentMousePosition;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame){
            manager.tutorialText.text = "Aperte Espaço para avançar.";
            isRotating = false;
        }

        manager.protoboard.transform.rotation = Quaternion.Slerp(
            manager.protoboard.transform.rotation,
            targetRotation,
            dampingSpeed * Time.deltaTime
        );

        if (Keyboard.current.spaceKey.wasPressedThisFrame){
            Debug.Log("4º Step Concluido");
            manager.SetStep(StepsManager.TutorialStep.FacePlayerStep);
        }
    }

    public void Exit(StepsManager manager){
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked; 
    }
}