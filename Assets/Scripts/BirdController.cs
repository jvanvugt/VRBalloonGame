using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    private GameObject balloon;
    private new ParticleSystem particleSystem;
    private MeshRenderer[] renderers;
    private BalloonFlightSystem flightSystem;
    private new Rigidbody rigidbody;
    public float speed = 10f;
    private AudioSource balloonPopSound;
    private PointTracker pointTracker;
    public GameObject arrow;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        balloon = GameObject.Find("BirdTarget");
        particleSystem = GetComponent<ParticleSystem>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        flightSystem = GameObject.FindWithTag("Balloon").GetComponent<BalloonFlightSystem>();
        balloonPopSound = GameObject.Find("balloon").GetComponent<AudioSource>();
        pointTracker = GameObject.FindWithTag("Balloon").GetComponent<PointTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            transform.LookAt(balloon.transform);
            rigidbody.velocity = transform.forward * speed;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        print(coll.gameObject.name);
        if (coll.gameObject.name == "balloon")
        {
            print("bird attac");
            pointTracker.score -= 5f;
            dead = true;
            foreach(var renderer in renderers)
                renderer.enabled = false;
            flightSystem.coolDownSpeed += 0.0025f;
            particleSystem.Play();
            balloonPopSound.Play();
            Destroy(gameObject, 0.5f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bullet")
        {
            print("bird is kil");
            pointTracker.score += 10f;
            dead = true;
            foreach(var renderer in renderers)
                renderer.enabled = false;
            particleSystem.Play();
            Destroy(gameObject, 0.5f);
            Destroy(col.gameObject);
        }
    }
}
