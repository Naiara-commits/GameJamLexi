using UnityEngine;

public class ScaleBreathing2D : MonoBehaviour
{
    [SerializeField, Range(0f, 0.2f)] private float scaleVariation = 0.03f;
    [SerializeField, Min(0f)] private float cyclesPerSecond = 0.8f;
    [SerializeField, Min(1f)] private float movementSpeedMultiplier = 2f;

    private Vector3 initialScale;
    private float phase;
    private bool isMoving;

    private void Awake()
    {
        initialScale = transform.localScale;
        phase = Time.time * cyclesPerSecond * 2f * Mathf.PI;
    }

    private void Update()
    {
        float currentCyclesPerSecond = cyclesPerSecond * (isMoving ? movementSpeedMultiplier : 1f);
        phase += Time.deltaTime * currentCyclesPerSecond * 2f * Mathf.PI;
        float pulse = Mathf.Sin(phase);
        transform.localScale = initialScale * (1f + pulse * scaleVariation);
    }

    public void SetMoving(bool moving)
    {
        isMoving = moving;
    }

    private void OnDisable()
    {
        transform.localScale = initialScale;
    }
}
