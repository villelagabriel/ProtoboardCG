using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour{
    public enum TutorialStep{
        LookAround,
        FocusOnProtoboard,
        CompleteTutorial
    }

    private PlayerControls controls;
    private Vector2 lastLookInput;
    private bool focusPressed;

    public GameObject protoboard;
    public TextMeshProUGUI tutorialText; 

    private TutorialStep currentStep;

    private bool hasFocused = false;

    private bool isFocusing = false;
    private float focusSpeed = 2f;

    void Awake(){
        controls = new PlayerControls();
        controls.Player.Look.performed += ctx => lastLookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lastLookInput = Vector2.zero;
        controls.Player.Focus.performed += ctx => focusPressed = true;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start(){
        currentStep = TutorialStep.LookAround;
        StartTutorial();
    }

    void StartTutorial(){
        protoboard.SetActive(false);
        tutorialText.text = "Olhe ao redor para começar o tutorial.";
    }

    void Update(){
        switch (currentStep){
            case TutorialStep.LookAround:
                CheckLookAround();
                break;
            case TutorialStep.FocusOnProtoboard:
                CheckFocusOnProtoboard();
                break;
            case TutorialStep.CompleteTutorial:
                CompleteTutorial();
                break;
        }

        if (isFocusing){
            SmoothFocusCameraOnProtoboard();
        }

        focusPressed = false;
    }

    private float lookAroundCurrent = 0f;
    private float lookAroundLimit = 100f;
    
    void CheckLookAround(){
        if (lastLookInput.magnitude > 0.1f) {
            lookAroundCurrent += lastLookInput.magnitude / 100;
            Debug.Log("Acúmulo de movimento: " + lookAroundCurrent.ToString("F2"));

            if (lookAroundCurrent >= lookAroundLimit) {
                currentStep = TutorialStep.FocusOnProtoboard;
                protoboard.SetActive(true);
                tutorialText.text = "Pressione 'F' para focar na Protoboard.";
            }
        }
    }

    void CheckFocusOnProtoboard(){
        if (focusPressed && !hasFocused){
            FocusCameraOnProtoboard();
            hasFocused = true;
            tutorialText.text = "Você focou na Protoboard!";
            currentStep = TutorialStep.CompleteTutorial;
        }
    }

    void SmoothFocusCameraOnProtoboard(){
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

    void CompleteTutorial(){
        tutorialText.text = "Parabéns! Você completou o tutorial.";
    }

     void FocusCameraOnProtoboard(){
        SetCameraControl(false);
        isFocusing = true;
    }

    void SetCameraControl(bool value){
        var cameraControl = Camera.main.GetComponent<MouseLookNewInput>(); 
        if (cameraControl != null)
            cameraControl.enabled = value;
    }

}