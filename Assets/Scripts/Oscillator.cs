using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 4f;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Continually growing overt ime
        float cyles = Time.time / period;

        // Constant value of 6.283
        const float tau = Mathf.PI * 2;
        // Going from -1 to 1
        float rawSinWave = Mathf.Sin(tau * cyles);
        // float rawSinWave = (rawSineWave + 1f) / 2f;

        // Recalculated to go from 0 to 1 so its cleaner
        movementFactor = Mathf.Abs(rawSinWave);


        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
