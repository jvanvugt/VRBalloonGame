using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    private BalloonFlightSystem balloon;

    // Start is called before the first frame update
    void Start()
    {
        balloon = GetComponent<BalloonFlightSystem>();
    }

    // Update is called once per framea
    void FixedUpdate()
    {
        if (Input.GetKey("space"))
        {
            balloon.heat += 0.01f;
        }
    }
}
