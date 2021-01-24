using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonController : MonoBehaviour
{
    private BalloonFlightSystem balloon;
    public bool throttleActive = false;
    private ParticleSystem ps;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        balloon = GetComponent<BalloonFlightSystem>();
        ps = GetComponentInChildren<ParticleSystem>();
        audioSource = ps.gameObject.GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (throttleActive) {
            balloon.heat = Mathf.Clamp(balloon.heat + 0.05f, 20, 150);
            if (!ps.isPlaying)
            {
                ps.Play();
                audioSource.Play();
            }
        }
        else
        {
            ps.Stop();
            audioSource.Stop();
        }

    }

    private void OnReset()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnThrottle()
    {
        throttleActive = !throttleActive;
    }
}
