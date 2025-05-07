using UnityEngine;

public class Third_ApproachProtoboardStep : ITutorialStep{
    private bool isApproaching = true;
    private Vector3 targetPosition;
    private float speed = 2f;

    public void Enter(StepsManager manager){
        Debug.Log("3º Step Iniciado");

        Transform cam = Camera.main.transform;
        targetPosition = cam.position + cam.forward * 1.5f;
    }

    public void Update(StepsManager manager){

        if (isApproaching){
            manager.protoboard.transform.position = Vector3.MoveTowards(
                manager.protoboard.transform.position,
                targetPosition,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(manager.protoboard.transform.position, targetPosition) < 0.1f){
                isApproaching = false;
                Debug.Log("3º Step Concluido");
                manager.SetStep(StepsManager.TutorialStep.RotateProtoboard);
            }
        }
    }

    public void Exit(StepsManager manager) { }
}
