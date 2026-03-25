using System.Collections;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    [Header("Movement Limits")]
    public float minY;
    public float maxY;

    [Header("Movement Settings")]
    public float speed = 3f;

    [Header("Pause Settings")]
    public float minMoveTime = 1f;
    public float maxMoveTime = 3f;
    public float minPauseTime = 0.5f;
    public float maxPauseTime = 2f;

    private int direction = 1; // 1 = arriba, -1 = abajo
    private bool isMoving = true;
    
    void Start()
    {
        StartCoroutine(MovementLoop());
    }

    void Update()
    {
        if (!isMoving) return;

        Vector3 pos = transform.position;
        pos.y += direction * speed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

        if (pos.y >= maxY)
            direction = -1;
        else if (pos.y <= minY)
            direction = 1;
    }

    private IEnumerator MovementLoop()
    {
        while (true)
        {
            float moveTime = Random.Range(minMoveTime, maxMoveTime);
            isMoving = true;
            yield return new WaitForSeconds(moveTime);

            isMoving = false;
            float pauseTime = Random.Range(minPauseTime, maxPauseTime);
            yield return new WaitForSeconds(pauseTime);

            direction = Random.value > 0.5f ? 1 : -1;
        }
    }
}
