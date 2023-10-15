using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingTriggers : MonoBehaviour
{

    AudioSource levelCompleteSound;
    bool isTransitioning = false;

    void Start()
    {
        levelCompleteSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) {
        if ((other.gameObject.tag == "Player") && (isTransitioning == false)) {
            levelCompleteSound.Stop();
            levelCompleteSound.Play();
            isTransitioning = true;
        }       
    }
}
