using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class StepsManager : MonoBehaviour{
    public enum TutorialStep{
        LookAround,
        FocusOnProtoboard,
        ApproachProtoboard,
        RotateProtoboard,
        FacePlayerStep,
        ResetProtoboardStep,
        OhmsLawExplanationStep,
        CompleteTutorial
    }

    public InputActionAsset actions;
    private PlayerControls controls;
    private Vector2 lastLookInput;
    private bool focusPressed;

    public GameObject protoboard;
    public GameObject resistor;
    public TextMeshProUGUI tutorialText;
    
    public GameObject trilhasDaProtoboardInternas;
    public GameObject trilhasDaProtoboardExternas;

    private ITutorialStep currentStep;
    private bool isFocusing = false;
    private float focusSpeed = 2f;

    void Awake(){
        controls = new PlayerControls();
        controls.Player.Look.performed += ctx => lastLookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lastLookInput = Vector2.zero;
        controls.Player.Focus.performed += ctx => focusPressed = true;

        trilhasDaProtoboardInternas.SetActive(false);
        trilhasDaProtoboardExternas.SetActive(false);
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start(){
        SetStep(TutorialStep.LookAround);
    }

    void Update(){
        currentStep?.Update(this);
        if (isFocusing){
            SmoothFocusCameraOnProtoboard();
        }
        focusPressed = false;
    }

    public void SetStep(TutorialStep step){
        currentStep?.Exit(this);
        switch (step){
            case TutorialStep.LookAround:
                currentStep = new First_LookAroundStep();
                break;
            case TutorialStep.FocusOnProtoboard:
                currentStep = new Second_FocusOnProtoboardStep();
                break;
            case TutorialStep.ApproachProtoboard:
                currentStep = new Third_ApproachProtoboardStep();
                break;
            case TutorialStep.RotateProtoboard:
                currentStep = new Fourth_RotateProtoboardStep();
                break;
            case TutorialStep.FacePlayerStep:
                currentStep = new Fifth_FacePlayerStep();
                break;
            case TutorialStep.ResetProtoboardStep:
                currentStep = new Sixth_ResetProtoboardStep();
                break;
            case TutorialStep.OhmsLawExplanationStep:
                currentStep = new Sixth_OhmsLawExplanationStep();
                break;
            case TutorialStep.CompleteTutorial:
                currentStep = new CompleteTutorialStep();
                break;
        }
        currentStep?.Enter(this);
    }

    public Vector2 GetLastLookInput() => lastLookInput;
    public bool GetFocusPressed() => focusPressed;

    public void StartFocusing() => isFocusing = true;
    public void StopFocusing() => isFocusing = false;

    public void SetCameraControl(bool value){
        var cameraControl = Camera.main.GetComponent<MouseLookNewInput>();
        if (cameraControl != null)
            cameraControl.enabled = value;
    }

    public bool IsFocusing() => isFocusing;

    private void SmoothFocusCameraOnProtoboard(){
        Transform camTransform = Camera.main.transform;
        Vector3 directionToLook = protoboard.transform.position - camTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);

        camTransform.rotation = Quaternion.RotateTowards(
            camTransform.rotation,
            targetRotation,
            focusSpeed * Time.deltaTime * 100f
        );

        if (Quaternion.Angle(camTransform.rotation, targetRotation) < 0.1f){
            isFocusing = false;
        }
    }
}
