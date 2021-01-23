using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnController : MonoBehaviour
{

    public float maxY;
    public float fireThreshold = 0.5f;

    public float heatSpeed = 0.1f;
    public float resetSpeed = 0.1f;
    private float startY;
    private bool holdingHandle = false;

    private bool wasBurningLastFrame = false;
    private BalloonFlightSystem balloon;
    public ParticleSystem firePS;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        var handleGameObject = transform.Find("hori").gameObject;
        balloon = GetComponentInParent<BalloonFlightSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float newYPos;
        if (holdingHandle)
        {

        }
        else
        {
            newYPos = transform.position.y + resetSpeed;
        }
        newYPos = Mathf.Clamp(transform.position.y, maxY, startY);
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
        bool shouldBurn = (newYPos - maxY) / (startY - maxY) > fireThreshold;
        if (shouldBurn)
        {
            balloon.heat += heatSpeed * Time.deltaTime;
            if (!wasBurningLastFrame)
            {
                firePS.Play();
            }
            wasBurningLastFrame = true;
        }
        else
        {
            wasBurningLastFrame = false;
            if (wasBurningLastFrame)
            {
                firePS.Stop();
            }
        }
    }

    void OnThrottle()
    {
    }
}
