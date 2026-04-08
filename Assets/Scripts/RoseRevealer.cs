using System.Collections;
using UnityEngine;

public class RoseRevealer : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    public float waitTime = 2f;
    
    void Start()
    {
        StartCoroutine(RevealRose());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RevealRose()
    {
        yield return new WaitForSeconds(waitTime);

        sprite.enabled = true;
    }
}
