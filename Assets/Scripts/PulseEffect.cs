using UnityEngine;

public class PulseEffect : MonoBehaviour {
    public float pulseSpeed = 0.01f;
    public float pulseAmount = 0.005f;

    private Vector3 originalScale;

    void Start() {
        originalScale = transform.localScale;
    }

    void Update() {
        float scaleOffset = Mathf.PingPong(Time.time * pulseSpeed, pulseAmount);
        transform.localScale = originalScale + Vector3.one * scaleOffset;
    }
}
