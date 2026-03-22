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

    void Start()
    {
        initialScale = heartImage.localScale;
        initialColliderSize = boxCollider.size;
    }

    void Update()
    {
        heartRate = Mathf.Clamp(heartRate, minHeartRate, maxHeartRate);
        float t = Mathf.InverseLerp(minHeartRate, maxHeartRate, heartRate);


        float frequency = heartRate / 60f;

        float pulse = Mathf.Sin(Time.time * frequency * Mathf.PI * 2);

        float normalizedPulse = (pulse + 1f) / 2f;

        float scaleFactor = 1 + normalizedPulse * scaleAmplitude;
        heartImage.localScale = initialScale * scaleFactor;

        float colliderFactor = 1 + normalizedPulse * scaleAmplitude;
        currentColliderScale = initialColliderSize * colliderFactor;

        currentColliderScale = Vector2.Lerp(initialColliderSize, colliderMaxScale, t);

        colliderVisual.localScale = new Vector3(
            currentColliderScale.x,
            currentColliderScale.y,
            1f
        );
    }
}