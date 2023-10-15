using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource rocketSound;

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateSpeed = 2f;
    [SerializeField] AudioClip rocketThrustSound;

    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem mainThrustParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateMainThruster();
        }
        else {
            rocketSound.Stop();
            mainThrustParticles.Stop();
        }
    }

    void ActivateMainThruster()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }

        if (!rocketSound.isPlaying)
        {
            rocketSound.PlayOneShot(rocketThrustSound);
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(-rotateSpeed);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(rotateSpeed);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void ApplyRotation(float rotationKey)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationKey * Time.deltaTime);

        // Unfreezing rotation so the physics system can take over
        rb.freezeRotation = false;
    }
}
