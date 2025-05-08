using UnityEngine;
using UnityEngine.InputSystem;

public class Fifth_FacePlayerStep : ITutorialStep {
    private float dampingSpeed = 2f;
    private int messageIndex = 0;
    private string[] messages = new string[] {
        "Este objeto é chamado de protoboard.",
        "Você pode conectá-la com fios.",
        "Ela é usada para montar circuitos.",
        "Os furos estão conectados internamente.",
        "As trilhas internas são conectadas desta maneira:",
        "Tudo colocado aqui se conecta em cada linha vertical",
        "As trilhas internas são conectadas desta maneira:",
        "Tudo colocado aqui se conecta em na horizontal.",
    };

    private Camera mainCamera;

    public void Enter(StepsManager manager){
        Debug.Log("5º Etapa Iniciada: Protoboard virada para o jogador");
        manager.tutorialText.text = messages[messageIndex];
        mainCamera = Camera.main;
    }

    public void Update(StepsManager manager){
        // Rotaciona a protoboard para a câmera
        Transform camTransform = mainCamera.transform;
        Vector3 directionToLook = camTransform.position - manager.protoboard.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x + 90f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

        manager.protoboard.transform.rotation = Quaternion.Slerp(
            manager.protoboard.transform.rotation,
            targetRotation,
            dampingSpeed * Time.deltaTime
        );

        if (Keyboard.current.fKey.wasPressedThisFrame){
            messageIndex++;

            if (messageIndex < messages.Length){
                manager.tutorialText.text = messages[messageIndex];
            } else {
                Debug.Log("5º Etapa Concluída");
                manager.trilhasDaProtoboardExternas.SetActive(false);
                manager.SetStep(StepsManager.TutorialStep.ResetProtoboardStep);
            }

            if(messageIndex == 4) {
                manager.trilhasDaProtoboardInternas.SetActive(true);
            }
            if(messageIndex == 7) {
                manager.trilhasDaProtoboardInternas.SetActive(false);
                manager.trilhasDaProtoboardExternas.SetActive(true);
            }
        }
    }

    public void Exit(StepsManager manager){
        // Pode resetar variáveis ou limpar textos, se necessário
    }
}
