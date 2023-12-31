using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) {return;}

        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // tau == 2pi
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1) / 2; // Recalculated to be between 0 - 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset; 
    }
}
