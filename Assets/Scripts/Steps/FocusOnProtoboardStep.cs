using UnityEngine;

public class FocusOnProtoboardStep : ITutorialStep
{
    private bool hasFocused = false;

    public void Enter(StepsManager manager)
    {
        manager.protoboard.SetActive(true);
        manager.tutorialText.text = "Pressione 'F' para focar na Protoboard.";
    }

    public void Update(StepsManager manager)
    {
        if (manager.GetFocusPressed() && !hasFocused)
        {
            manager.SetCameraControl(false);
            manager.StartFocusing();
            hasFocused = true;
            manager.tutorialText.text = "Você focou na Protoboard!";
            manager.SetStep(StepsManager.TutorialStep.CompleteTutorial);
        }
    }

    public void Exit(StepsManager manager) { }
}