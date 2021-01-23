using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlightSystem : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private PointTracker pointTracker;
    private AudioSource destructionSound;

    public float heat = 1.0f;
    public Vector2 wind = new Vector2(0, 1);

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
        heat -= 0.001f;

        // Change position
        rigidbody.velocity = new Vector3(wind.x, heat, wind.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        pointTracker.crashed = true;
        destructionSound.Play();
    }
}
