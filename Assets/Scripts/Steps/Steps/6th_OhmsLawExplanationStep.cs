using UnityEngine;
using UnityEngine.InputSystem;

public class Sixth_OhmsLawExplanationStep : ITutorialStep{
  private float dampingSpeed = 10f;
  private Camera mainCamera;
  private float previousFontSize;
  private bool isRotating = false; 
  private Vector2 lastMousePosition;
  private float rotationSpeed = 1.0f; 
  private Vector3 targetRotationEuler; 
  private Quaternion targetRotation; 

  public void Enter(StepsManager manager){
    Debug.Log("6º Etapa Iniciada: explicação da lei de Ohm");
    manager.resistor.SetActive(true);
    
    previousFontSize = manager.tutorialText.fontSize;
    manager.tutorialText.fontSize = 20f;
    manager.tutorialText.text = "Lei de Ohm \n A lei de Ohm afirma que a resistência elétrica é determinada pela razão entre o potencial elétrico e a corrente elétrica \n R = V/I";

    Cursor.visible = true;

    mainCamera = Camera.main;
    targetRotation = manager.protoboard.transform.rotation;
    targetRotationEuler = targetRotation.eulerAngles;

    mainCamera = Camera.main;
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
      isRotating = false;
    }

   manager.resistor.transform.rotation = Quaternion.Slerp(
      manager.resistor.transform.rotation,
      targetRotation,
      dampingSpeed * Time.deltaTime
    );

    // Proceed to next step on spacebar press
    if (Keyboard.current.spaceKey.wasPressedThisFrame){
      manager.tabelaCores.SetActive(true);
      manager.tutorialText.fontSize = previousFontSize;
      manager.tutorialText.text = "Consultando a tabela de cores de resistores, qual é o valor da resistência elétrica do resistor mostrado?";

      Debug.Log("6º Etapa Concluída");
      //manager.SetStep(StepsManager.TutorialStep.CompleteTutorial);
    }
  }


  public void Exit(StepsManager manager)
  {
    // Nada específico para sair, mas você pode limpar ou restaurar valores aqui se necessário
  }
}
