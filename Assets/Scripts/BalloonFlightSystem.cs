using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BalloonFlightSystem : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private PointTracker pointTracker;
    private AudioSource destructionSound;
    private AudioSource warningSound;
    private AudioSource balloonExplodedSound;
    private GameObject balloonModel;

    public float heat = 60.0f;
    public Vector2 wind = new Vector2(0, 1);
    public TextMeshProUGUI heatText;
    public float coolDownSpeed = 0.01f;

    public bool balloonIsBroken = false;
    private bool isResetting;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        pointTracker = GetComponent<PointTracker>();
        destructionSound = GameObject.Find("DestructionSound").GetComponent<AudioSource>();
        warningSound = GameObject.Find("WarningSound").GetComponent<AudioSource>();
        balloonExplodedSound = GameObject.Find("BalloonExplodedSound").GetComponent<AudioSource>();
        balloonModel = GameObject.Find("balloon");
    }

    void FixedUpdate()
    {
        // Reduce heat
        heat = Mathf.Clamp(heat - coolDownSpeed, 20, 150);
        heatText.SetText($"Heat: {heat:F0}°C");

        // Check if heat is too high
        if (heat > 130 || balloonIsBroken)
        {
            // Initiate crashing procedure
            if (!balloonExplodedSound.isPlaying && !balloonIsBroken)
            {
                balloonExplodedSound.Play();
            }

            balloonModel.GetComponentInChildren<ParticleSystem>().Play();
            balloonIsBroken = true;
            rigidbody.velocity = new Vector3(wind.x, -3, wind.y);
        }
        else
        {
            // Change position
            rigidbody.velocity = new Vector3(wind.x, (heat - 60) / 50, wind.y);
        }

        if (heat > 120)
        {
            if (!warningSound.isPlaying)
            {
                warningSound.Play();
            }
        }
        else
        {
            warningSound.Stop();
        }
    }

    private IEnumerator ResetGame()
    {
        if (isResetting)
        {
            yield break;
        }

        isResetting = true;

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("SampleScene");
        isResetting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        pointTracker.crashed = true;
        if (!destructionSound.isPlaying)
        {
            destructionSound.Play();
        }
        warningSound.Stop();

        StartCoroutine(ResetGame());
    }
}
