using UnityEngine;

public class LookAroundStep : ITutorialStep
{
    private float lookAroundCurrent = 0f;
    private float lookAroundLimit = 100f;

    public void Enter(StepsManager manager)
    {
        manager.protoboard.SetActive(false);
        manager.tutorialText.text = "Olhe ao redor para começar o tutorial.";
    }

    public void Update(StepsManager manager)
    {
        Vector2 lookInput = manager.GetLastLookInput();
        if (lookInput.magnitude > 0.1f)
        {
            lookAroundCurrent += lookInput.magnitude / 100;
            Debug.Log("Acúmulo de movimento: " + lookAroundCurrent.ToString("F2"));

            if (lookAroundCurrent >= lookAroundLimit)
            {
                manager.SetStep(StepsManager.TutorialStep.FocusOnProtoboard);
            }
        }
    }

    public void Exit(StepsManager manager) { }
}