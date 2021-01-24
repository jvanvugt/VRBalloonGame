using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public GameObject arrow;
    private UnityEngine.XR.InputDevice rightController;
    private Rigidbody balloonrb;
    private float lastShot = 0f;
    private float cooldown = 0.5f;
    public float shootSpeed = 70f;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);
        if (rightHandedControllers.Count > 0)
            rightController = rightHandedControllers[0];
        balloonrb = GameObject.FindWithTag("Balloon").GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool triggerPressed;
        if (rightController != null && rightController.IsPressed(InputHelpers.Button.Trigger, out triggerPressed) && triggerPressed)
        {
            if (Time.time - lastShot > cooldown)
            {
                lastShot = Time.time;
                var spawnedArrow = Instantiate(arrow, transform.position, transform.rotation);
                var arrowRb = spawnedArrow.GetComponentInChildren<Rigidbody>();
                arrowRb.velocity = balloonrb.velocity;
                arrowRb.velocity += transform.forward * shootSpeed;
                Destroy(spawnedArrow, 10f);
                audioSource.Play();
            }
        }
    }
}
