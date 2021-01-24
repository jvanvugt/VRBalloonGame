using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonController : MonoBehaviour
{
    private BalloonFlightSystem balloon;
    private bool throttleActive = false;

    // Start is called before the first frame update
    void Start()
    {
        balloon = GetComponent<BalloonFlightSystem>();
    }
    private void FixedUpdate()
    {
        if (throttleActive) {
            balloon.heat = Mathf.Clamp(balloon.heat + 0.05f, 20, 150);
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
