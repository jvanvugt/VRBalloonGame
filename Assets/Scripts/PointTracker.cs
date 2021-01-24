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
            score += Mathf.Sqrt(Mathf.Pow(transform.position.x - previousPosition.x, 2) + Mathf.Pow(transform.position.z - previousPosition.z, 2));
            previousPosition = transform.position;
        }

        scoreText.SetText($"Score: {score:F0}");
    }
}
