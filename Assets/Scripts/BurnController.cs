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
    private BalloonController balloon;
    public ParticleSystem firePS;
    private UnityEngine.XR.InputDevice leftController;
    private GameObject leftControllerGO;
    private UnityEngine.XR.InputDevice rightController;
    private bool lastLeftState;
    private GameObject handleGameObject;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        handleGameObject = transform.Find("hori").gameObject;
        balloon = GetComponentInParent<BalloonController>();

        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);
        if (leftHandedControllers.Count > 0)
        {
            leftController = leftHandedControllers[0];
            leftControllerGO = transform.Find("LeftController").gameObject;
        }

        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);
        if (leftHandedControllers.Count > 0)
        {
            rightController = rightHandedControllers[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        float newYPos;
        bool holdingHandle = false;
        bool triggerValue = false;
        if (leftController != null && leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out triggerValue) && triggerValue)
        {
            if ((leftControllerGO.transform.position - handleGameObject.transform.position).magnitude < 0.4f)
            {
                holdingHandle = true;
                newYPos = leftControllerGO.transform.position.y;
            }
        }

        if (Input.GetKey("space"))
        {
            holdingHandle = true;
            newYPos = maxY;
        }

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
            balloon.throttleActive = true;
            if (!wasBurningLastFrame)
            {
                firePS.Play();
            }
            wasBurningLastFrame = true;
        }
        else
        {
            balloon.throttleActive = false;
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
