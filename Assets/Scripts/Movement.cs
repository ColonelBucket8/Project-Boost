using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1000f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;
    [SerializeField] ParticleSystem mainEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Caching
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }



    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotate();
        }
    }

    void StartThrusting()
    {

        // To make sure we only play if we aren't already playing
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }

        rb.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
    }

    private void StopThrusting()
    {
        mainEngineParticles.Stop();
        audioSource.Stop();
    }


    void RotateRight()
    {
        if (!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }


        ApplyRotation(-rotationThrust);
    }

    void RotateLeft()
    {
        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }


        ApplyRotation(rotationThrust);
    }

    private void StopRotate()
    {
        leftEngineParticles.Stop();
        rightEngineParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // Freezing rotation so we can manually rotate
        rb.freezeRotation = true;
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);

        // Unfreezing rotation so the physics system can take over
        rb.freezeRotation = false;

    }

}
