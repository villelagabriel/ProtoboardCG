using UnityEngine;

public class Second_FocusOnProtoboardStep : ITutorialStep
{
    private bool hasFocused = false;

    public void Enter(StepsManager manager){
        Debug.Log("2 Step Iniciado");
        manager.protoboard.SetActive(true);
        manager.tutorialText.text = "Pressione 'F' para focar na Protoboard.";
    }

    public void Update(StepsManager manager){
        if (manager.GetFocusPressed() && !hasFocused){
            manager.SetCameraControl(false);
            manager.StartFocusing();
            hasFocused = true;
            manager.tutorialText.text = "Você focou na Protoboard!";
        }

        if (hasFocused && !manager.IsFocusing()){
            Debug.Log("2 Step Concluido");
            manager.SetStep(StepsManager.TutorialStep.ApproachProtoboard);
        }
    }

    public void Exit(StepsManager manager) { }
}