using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BurnController : MonoBehaviour
{

    private float maxY;
    public float fireThreshold = 0.5f;

    public float heatSpeed = 0.1f;
    public float resetSpeed = 0.2f;
    private float startY;

    private BalloonController balloon;
    private UnityEngine.XR.InputDevice leftController;
    private GameObject leftControllerGO;
    private UnityEngine.XR.InputDevice rightController;
    private GameObject handleGameObject;
    private TextMeshProUGUI debugText;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.localPosition.y;
        maxY = startY - 0.25f;
        handleGameObject = transform.Find("hori").gameObject;
        balloon = GetComponentInParent<BalloonController>();
        debugText = GameObject.Find("debugText").GetComponent<TextMeshProUGUI>();
        Debug.Log(debugText);

        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);
        if (leftHandedControllers.Count > 0)
        {
            leftController = leftHandedControllers[0];
            leftControllerGO = GameObject.Find("LeftController");
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
        float newYPos = transform.localPosition.y;
        bool holdingHandle = false;
        bool gripValue;
        bool gripPressedValue = false;
        if (leftController != null &&
            (leftController.IsPressed(InputHelpers.Button.Grip, out gripValue) ||
            leftController.IsPressed(InputHelpers.Button.GripPressed, out gripPressedValue)) &&
            (gripValue || gripPressedValue))
        {
            if ((leftControllerGO.transform.position - handleGameObject.transform.position).magnitude < 0.5f)
            {
                holdingHandle = true;
                newYPos = transform.InverseTransformPoint(leftControllerGO.transform.position).y + handleGameObject.transform.localPosition.y;
                debugText.SetText($"Holding, new Pos: {newYPos}. Cur Pos: {transform.localPosition.y}");
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
            newYPos = transform.localPosition.y + resetSpeed * Time.deltaTime;
        }
        newYPos = Mathf.Clamp(newYPos, maxY, startY);

        Debug.Log(newYPos);

        transform.localPosition = new Vector3(transform.localPosition.x, newYPos, transform.localPosition.z);
        bool shouldBurn = (newYPos - maxY) / (startY - maxY) < fireThreshold;
        balloon.throttleActive = shouldBurn;
    }
}
