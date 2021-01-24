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
    public float speed = 8f;
    private AudioSource audioSource;
    public AudioClip balloonPopSound;
    public AudioClip birdDeadSound;
    private PointTracker pointTracker;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        balloon = GameObject.Find("BirdTarget");
        particleSystem = GetComponent<ParticleSystem>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        flightSystem = GameObject.FindWithTag("Balloon").GetComponent<BalloonFlightSystem>();
        audioSource = GameObject.Find("balloon").GetComponent<AudioSource>();
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
        if (!dead && coll.gameObject.name == "balloon")
        {
            print("bird attac");
            pointTracker.score -= 200f;
            dead = true;
            foreach(var renderer in renderers)
                renderer.enabled = false;
            flightSystem.coolDownSpeed += 0.0025f;
            particleSystem.Play();
            audioSource.clip = balloonPopSound;
            audioSource.Play();
            Destroy(gameObject, 0.5f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bullet")
        {
            print("bird is kil");
            pointTracker.score += 100f;
            dead = true;
            foreach(var renderer in renderers)
                renderer.enabled = false;
            particleSystem.Play();
            audioSource.clip = birdDeadSound;
            audioSource.Play();
            Destroy(gameObject, 0.5f);
            Destroy(col.gameObject);
        }
    }
}
