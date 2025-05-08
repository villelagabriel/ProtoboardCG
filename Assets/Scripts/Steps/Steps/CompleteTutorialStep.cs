using UnityEngine;

public class CompleteTutorialStep : ITutorialStep
{
    public void Enter(StepsManager manager)
    {
        manager.tutorialText.text = "Parabéns! Você completou o tutorial.";
    }

    public void Update(StepsManager manager) { }

    public void Exit(StepsManager manager) { }
}
