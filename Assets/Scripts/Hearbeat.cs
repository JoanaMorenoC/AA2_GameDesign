using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    [Header("Heartbeat settings")]
    public float heartRate = 60f;
    public float scaleAmplitude = 0.2f;

    public float minHeartRate = 60f;
    public float maxHeartRate = 160f;

    [Header("Collider settings")]
    private Vector2 initialColliderSize;
    [SerializeField] private Vector2 colliderMaxScale;
    public Vector2 currentColliderScale;

    [Header("References")]
    public RectTransform heartImage;
    public BoxCollider2D boxCollider;
    public Transform colliderVisual;

    private Vector3 initialScale;
    private float phase = 0f;
    private float previousPulse = 0f;
    float lastScaleFactor = 1f;


    void Start()
    {
        initialScale = heartImage.localScale;
        initialColliderSize = boxCollider.size;
        colliderMaxScale = initialColliderSize * 2f;
    }


    void Update()
    {
        heartRate = Mathf.Clamp(heartRate, minHeartRate, maxHeartRate);
        float t = Mathf.InverseLerp(minHeartRate, maxHeartRate, heartRate);

        float frequency = heartRate / 60f;

        phase += Time.deltaTime * frequency * Mathf.PI * 2;

        float pulse = Mathf.Sin(phase);

        if (previousPulse < 0.9f && pulse >= 0.9f)
        {
            float volume = Mathf.Lerp(0.2f, 1f, t);
            SFXManager.Instance.PlayGlobalSound("Heartbeat", volume);
        }
        previousPulse = pulse;

        float normalizedPulse = (pulse + 1f) / 2f;

        float scaleFactor = 1 + normalizedPulse * scaleAmplitude;
        lastScaleFactor = scaleFactor;
        heartImage.localScale = initialScale * scaleFactor;

        currentColliderScale = Vector2.Lerp(initialColliderSize, colliderMaxScale, t);

        colliderVisual.localScale = new Vector3(
            currentColliderScale.x,
            currentColliderScale.y,
            1f
        );
    }

    public float GetScaleFactor()
    {
        return lastScaleFactor;
    }
}