using TMPro;
using UnityEngine;

public class ReprocheHider : MonoBehaviour
{
    private string currentText;
    private string prevText;

    private float sameTextTimer = 0f;
    [SerializeField] private float hideDelay = 5f;

    public TextMeshProUGUI txt;

    void Start()
    {
        prevText = txt.text;
    }

    void Update()
    {
        currentText = txt.text;

        if (currentText == prevText)
        {
            sameTextTimer += Time.deltaTime;

            if (sameTextTimer >= hideDelay)
            {
                txt.enabled = false;
            }
        }
        else
        {
            sameTextTimer = 0f;
            prevText = currentText;

            if (!txt.gameObject.activeSelf)
                txt.enabled = true;
        }
    }
}
