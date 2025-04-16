using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(PlayerInput))]
public class TankController : MonoBehaviour
{
    public float tankSpeed = 15f;
    public float tankRotationSpeed = 20f;
    public float turretLagSpeed = 0.5f;
    private Vector3 finalTurretLookDir;

    public Transform reticleTransform;
    public Transform turretTransform;

    public float centreOfGravityOffset = -1f;

    public GameObject[] leftWheels;
    public GameObject[] rightWheels;
    public float wheelRotationSpeed = 200.0f;

    private Rigidbody rigidBody;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        // Adjust center of mass to improve stability and prevent rolling
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;
    }
    private void Update()
    {
        HandleWheelRotation();
    }

    // FixedUpdate is called at a fixed time interval 
    void FixedUpdate()
    {
        if(rigidBody & playerInput)
        {
            HandleMovement();
            HandleTurret();
            //HandleReticle();
        }     
    }

    void HandleMovement()
    {
        //move tank forward
        Vector3 wantedPosition = rigidBody.position + ( transform.forward * playerInput.ForwardInput * tankSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(wantedPosition);

        // tank rotation
        Quaternion wantedRotation = rigidBody.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * playerInput.RotationInput * Time.fixedDeltaTime));
        rigidBody.MoveRotation(wantedRotation);
    }

    void HandleWheelRotation()
    {
        float wheelRotation = playerInput.ForwardInput * wheelRotationSpeed * Time.deltaTime;
        foreach (GameObject wheel in leftWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation * playerInput.RotationInput * wheelRotationSpeed * Time.deltaTime, 0, 0);
            }
        }
        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation + playerInput.RotationInput * wheelRotationSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    void HandleTurret()
    {
        if(turretTransform)
        {
            Vector3 turretLookDir = playerInput.ReticlePosition - turretTransform.position;
            turretLookDir.y = 0;
            finalTurretLookDir = Vector3.Lerp(finalTurretLookDir, turretLookDir, Time.deltaTime * turretLagSpeed);
            turretTransform.rotation = Quaternion.LookRotation(finalTurretLookDir);
        }
    }

    void HandleReticle()
    {
        if(reticleTransform)
        {
            reticleTransform.position = playerInput.ReticlePosition;
        }
    }
}
