using UnityEngine;
using TMPro;

public class BalloonFlightSystem : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private PointTracker pointTracker;
    private AudioSource destructionSound;

    public float heat = 60.0f;
    public Vector2 wind = new Vector2(0, 1);
    public TextMeshProUGUI heatText;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        pointTracker = GetComponent<PointTracker>();
        destructionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Reduce heat
        heat = Mathf.Clamp(heat - 0.01f, 20, 150);
        heatText.SetText($"Heat: {heat:F0}°C");

        // Change position
        rigidbody.velocity = new Vector3(wind.x, (heat - 60) / 50, wind.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        pointTracker.crashed = true;
        destructionSound.Play();
    }
}
