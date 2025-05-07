using UnityEngine;
using UnityEngine.InputSystem;

public class Fifth_FacePlayerStep : ITutorialStep {
    private float dampingSpeed = 2f; 
    private Camera mainCamera;

    public void Enter(StepsManager manager){
        Debug.Log("5º Etapa Iniciada: Protoboard virada para o jogador");
        manager.tutorialText.text = "A Protoboard agora deve ficar virada para você.";
        mainCamera = Camera.main;
    }

    public void Update(StepsManager manager){
        Transform camTransform = Camera.main.transform;
        Vector3 directionToLook = camTransform.position - manager.protoboard.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x + 90f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

        manager.protoboard.transform.rotation = Quaternion.Slerp(
            manager.protoboard.transform.rotation,
            targetRotation,
            dampingSpeed  * Time.deltaTime
        );

        // Proceed to next step on spacebar press
        if (Keyboard.current.spaceKey.wasPressedThisFrame){
            Debug.Log("5º Etapa Concluída");
            manager.SetStep(StepsManager.TutorialStep.CompleteTutorial);
        }
    }


    public void Exit(StepsManager manager){
        // Nada específico para sair, mas você pode limpar ou restaurar valores aqui se necessário
    }
}
