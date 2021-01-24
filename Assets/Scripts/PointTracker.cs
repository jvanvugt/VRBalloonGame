using UnityEngine;
using TMPro;

public class PointTracker : MonoBehaviour
{
    public float score;
    public bool crashed = false;
    public TextMeshProUGUI scoreText;

    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!crashed)
        {
            score += Mathf.Abs(transform.position.x - previousPosition.x) + Mathf.Abs(transform.position.z - previousPosition.z);
            previousPosition = transform.position;
        }

        scoreText.SetText($"Score: {score:F0}");
    }
}
