using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TextBubbleProjectile : MonoBehaviour
{
    public List<Sprite> textBubbles;

    public float destroyTime;

    public float speed = 6f;
    private Vector2 direction;

    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        int randomTextBubble = Random.Range(0, textBubbles.Count);

        spriteRenderer.sprite = textBubbles[randomTextBubble];
        Destroy(gameObject, destroyTime);

    }

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}
