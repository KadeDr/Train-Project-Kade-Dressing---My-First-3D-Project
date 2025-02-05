using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RocketController : Singleton<RocketController>
{

    [SerializeField] float thrustForce;
    [SerializeField] float turnForce;
    [SerializeField] AudioClip thrusterSFX;
    [SerializeField] ParticleSystem thrustFX;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] float zOffset;
    [SerializeField] float floatStrength;
    AudioSource audioSource;
    EnemyHandler enemyHandler;

    float turnInput;
    float thrustInput;

    Rigidbody myRigidbody;

    protected override void Awake()
    {
        base.Awake();
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        enemyHandler = GetComponent<EnemyHandler>();
    }

    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        Thrust();
        Torque();
    }

    void PlayerInput()
    {
        turnInput = Controls.Instance.rocketTorqueValue;
        thrustInput = Controls.Instance.rocketThrustValue;
    }

    void Thrust()
    {
        if (enemyHandler.dead) return;
        myRigidbody.AddRelativeForce(Vector3.up * thrustInput * thrustForce, ForceMode.Force);
        if (thrustInput != 0)
        {
            thrustFX.Play();
            if (audioSource.isPlaying) return;
            audioSource.PlayOneShot(thrusterSFX);
        }
        else
        {
            audioSource.Stop();
            thrustFX.Stop();
        }
    }

    void Torque()
    {
        myRigidbody.AddTorque(Vector3.forward * turnInput * -turnForce, ForceMode.Force);
    }
}
