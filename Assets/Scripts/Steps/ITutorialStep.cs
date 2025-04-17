using UnityEngine;

public interface ITutorialStep{
    void Enter(StepsManager manager);
    void Update(StepsManager manager);
    void Exit(StepsManager manager);
}