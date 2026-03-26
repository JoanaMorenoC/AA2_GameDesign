using UnityEngine;

public class AdaptShieldSize : MonoBehaviour
{
    [Header("References")]
    public Transform shieldTransform;
    public Heartbeat heartbeatScript;

    [Header("Optional settings")]
    public Vector2 shieldMaxScaleMultiplier = new Vector2(2f, 2f);

    private Vector3 initialScale;

    void Start()
    {
        initialScale = shieldTransform.localScale;
    }

    void Update()
    {
        float t = Mathf.InverseLerp(heartbeatScript.minHeartRate, heartbeatScript.maxHeartRate, heartbeatScript.heartRate);

        Vector2 targetScale = Vector2.Lerp(
            new Vector2(initialScale.x, initialScale.y),
            new Vector2(initialScale.x * shieldMaxScaleMultiplier.x, initialScale.y * shieldMaxScaleMultiplier.y),
            t
        );

        shieldTransform.localScale = new Vector3(targetScale.x, targetScale.y, initialScale.z);
    }
}