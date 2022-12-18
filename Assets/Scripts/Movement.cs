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

        else
        {
            mainEngineParticles.Stop();
            audioSource.Stop();
        }
    }


    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!rightEngineParticles.isPlaying)
            {
                rightEngineParticles.Play();
            }


            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            if (!leftEngineParticles.isPlaying)
            {
                leftEngineParticles.Play();
            }


            ApplyRotation(-rotationThrust);
        }

        else
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
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
