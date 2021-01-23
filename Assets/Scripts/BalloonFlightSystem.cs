using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlightSystem : MonoBehaviour
{
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var current = this.transform.position;
        var delta = Time.deltaTime * speed;
        this.transform.position = new Vector3(current.x, current.y, current.z + delta);
    }
}
