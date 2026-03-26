using System.Collections;
using UnityEngine;

public class ShieldSize : MonoBehaviour
{
    [Header("References")]
    public Transform shieldTransform;
    public Heartbeat heartbeatScript;

    [Header("Optional settings")]
    public Vector2 shieldMaxScaleMultiplier = new Vector2(2f, 2f);

    [Header("Appear Animation")]
    public float appearDuration = 0.15f;

    private Vector3 initialScale = Vector3.zero;

    private bool isAppearing = false;

    private void OnEnable()
    {
        if (initialScale == Vector3.zero)
            initialScale = shieldTransform.localScale;

        StartCoroutine(AppearAnimation());
    }

    void Update()
    {
        if (isAppearing) return;

        shieldTransform.localScale = GetCurrentTargetScale();
    }

    IEnumerator AppearAnimation()
    {
        isAppearing = true;

        float time = 0f;

        while (time < appearDuration)
        {
            time += Time.deltaTime;
            float tAnim = time / appearDuration;
            tAnim = Mathf.SmoothStep(0f, 1f, tAnim);

            Vector3 finalScale = GetCurrentTargetScale();

            shieldTransform.localScale = Vector3.Lerp(Vector3.zero, finalScale, tAnim);

            yield return null;
        }

        shieldTransform.localScale = GetCurrentTargetScale();
        isAppearing = false;
    }

    Vector3 GetCurrentTargetScale()
    {
        float t = Mathf.InverseLerp(
            heartbeatScript.minHeartRate,
            heartbeatScript.maxHeartRate,
            heartbeatScript.heartRate
        );

        Vector2 targetScale = Vector2.Lerp(
            new Vector2(initialScale.x, initialScale.y),
            new Vector2(initialScale.x * shieldMaxScaleMultiplier.x, initialScale.y * shieldMaxScaleMultiplier.y),
            t
        );

        return new Vector3(targetScale.x, targetScale.y, initialScale.z);
    }
}