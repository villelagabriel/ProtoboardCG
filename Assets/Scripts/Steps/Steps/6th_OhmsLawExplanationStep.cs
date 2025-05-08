using UnityEngine;
using UnityEngine.InputSystem;

public class Sixth_OhmsLawExplanationStep : ITutorialStep
{
  private float dampingSpeed = 2f;
  private Camera mainCamera;
  private float previousFontSize;

  public void Enter(StepsManager manager)
  {
    previousFontSize = manager.tutorialText.fontSize;
    Debug.Log("6º Etapa Iniciada: explicação da lei de Ohm");
    manager.tutorialText.fontSize = 20f;
    manager.tutorialText.text = "Lei de Ohm \n A lei de Ohm afirma que a resistência elétrica é determinada pela razão entre o potencial elétrico e a corrente elétrica \n R = V/I";

    Cursor.visible = true;

    mainCamera = Camera.main;
  }

  public void Update(StepsManager manager)
  {
    Transform camTransform = Camera.main.transform;
    Vector3 directionToLook = camTransform.position - manager.resistor.transform.position;

    Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
    targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x + 90f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

    manager.resistor.transform.rotation = Quaternion.Slerp(
        manager.resistor.transform.rotation,
        targetRotation,
        dampingSpeed * Time.deltaTime
    );

    // Proceed to next step on spacebar press
    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    {
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
