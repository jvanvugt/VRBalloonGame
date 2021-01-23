using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

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
            balloon.heat += 0.01f;
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
