using UnityEngine;

public class Sixth_ResetProtoboardStep : ITutorialStep {
    private Vector3 targetPosition = new Vector3(-0.6070499f, 2.183581f, -3.403715f);
    private Quaternion targetRotation = Quaternion.Euler(0, 180, 0);

    private float moveSpeed = 2f;
    private float rotateSpeed = 2f;

    private Camera mainCamera;
    private Vector3 cameraAboveOffset = new Vector3(0, 1.5f, 0); 
    private float cameraMoveSpeed = 2f;
    private float cameraRotateSpeed = 3f;

    public void Enter(StepsManager manager){
        Debug.Log("6º Etapa Iniciada: Reposicionando a protoboard");
        manager.tutorialText.text = "Ela tem este simbolo pois é aqui onde conectamos o terra e a fonte.";

        mainCamera = Camera.main;
    }

    public void Update(StepsManager manager){

        // Move a protoboard
        manager.protoboard.transform.position = Vector3.MoveTowards(
            manager.protoboard.transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Rotaciona a protoboard
        manager.protoboard.transform.rotation = Quaternion.RotateTowards(
            manager.protoboard.transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime * 100f
        );

        // Posiciona a câmera bem acima da protoboard
        Vector3 desiredCameraPosition = targetPosition + cameraAboveOffset;
        mainCamera.transform.position = Vector3.Lerp(
            mainCamera.transform.position,
            desiredCameraPosition,
            cameraMoveSpeed * Time.deltaTime
        );

        // Faz a câmera olhar diretamente para baixo
        Quaternion lookDownRotation = Quaternion.Euler(90f, 0f, 0f);
        mainCamera.transform.rotation = Quaternion.Slerp(
            mainCamera.transform.rotation,
            lookDownRotation,
            cameraRotateSpeed * Time.deltaTime
        );

        // Verifica se a protoboard e a câmera chegaram aos destinos
        bool protoboardDone =
            Vector3.Distance(manager.protoboard.transform.position, targetPosition) < 0.01f &&
            Quaternion.Angle(manager.protoboard.transform.rotation, targetRotation) < 0.5f;

        bool cameraDone =
            Vector3.Distance(mainCamera.transform.position, desiredCameraPosition) < 0.05f &&
            Quaternion.Angle(mainCamera.transform.rotation, lookDownRotation) < 1f;

        if (protoboardDone && cameraDone) {
            manager.fonte.SetActive(true);
            manager.jumpers_fonte.SetActive(true);
            Debug.Log("6º Etapa Concluída: Protoboard e câmera posicionadas");

            manager.SetStep(StepsManager.TutorialStep.OhmsLawExplanationStep);
        }
    }

    public void Exit(StepsManager manager){
        // Nada a limpar por enquanto
    }
}
