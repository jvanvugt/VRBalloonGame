﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlightSystem : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float heat = 1.0f;
    public Vector2 wind = new Vector2(0, 1);

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Reduce heat
        heat -= 0.001f;

        // Change position
        rigidbody.velocity = new Vector3(wind.x, heat, wind.y);
    }
}
