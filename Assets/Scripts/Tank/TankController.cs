using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NetworkTransform))]
public class TankController : NetworkBehaviour
{
    [Header("Tank Properties")]
    public float tankSpeed = 2f;
    public float tankRotationSpeed = 2f;
    public float turretLagSpeed = 0.5f;
    private Vector3 finalTurretLookDir;

    public Transform reticleTransform;
    public Transform turretTransform;

    public float centreOfGravityOffset = -1f;

    public GameObject[] leftWheels;
    public GameObject[] rightWheels;
    public float wheelRotationSpeed = 200.0f;

    private Rigidbody rigidBody;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            GameObject virtualCamera = GameObject.Find("Virtual Camera");
            if (virtualCamera != null)
            {
                virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = transform;
            }
        }
        //rigidBody = GetComponent<Rigidbody>();     

        //// Adjust center of mass to improve stability and prevent rolling
        //Vector3 centerOfMass = rigidBody.centerOfMass;
        //centerOfMass.y += centreOfGravityOffset;
        //rigidBody.centerOfMass = centerOfMass;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput<NetworkInputData>(out var input))
        {
            if(HasInputAuthority)
            {
                HandleMovement(input);
                HandleWheelRotation(input);
                HandleTurret(input);
                //HandleReticle(input);
            }
        }
    }

    void HandleMovement(NetworkInputData input)
    {
        ////move tank forward
        //Vector3 wantedPosition = rigidBody.position + (transform.forward * input.forward * tankSpeed * Runner.DeltaTime);
        //rigidBody.MovePosition(wantedPosition);

        //// tank rotation
        //Quaternion wantedRotation = rigidBody.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.rotation * Runner.DeltaTime));
        //rigidBody.MoveRotation(wantedRotation);

        // Move the tank
        Vector3 forwardMovement = transform.forward * input.forward * tankSpeed * Runner.DeltaTime;
        transform.position += forwardMovement;

        // Rotate the tank
        transform.Rotate(Vector3.up, input.rotation * tankRotationSpeed * Runner.DeltaTime);

    }

    void HandleWheelRotation(NetworkInputData input)
    {
        float wheelRotation = input.forward * wheelRotationSpeed * Runner.DeltaTime;
        foreach (GameObject wheel in leftWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation * input.rotation * wheelRotationSpeed * Runner.DeltaTime, 0, 0);
            }
        }
        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation + input.rotation * wheelRotationSpeed * Runner.DeltaTime, 0, 0);
            }
        }
    }

    void HandleTurret(NetworkInputData input)
    {
        if(turretTransform)
        {
            Vector3 turretLookDir = input.reticlePosition - turretTransform.position;
            turretLookDir.y = 0;
            finalTurretLookDir = Vector3.Lerp(finalTurretLookDir, turretLookDir, Runner.DeltaTime * turretLagSpeed);
            turretTransform.rotation = Quaternion.LookRotation(finalTurretLookDir);
        }
    }

    void HandleReticle(NetworkInputData input)
    {
        if(reticleTransform)
        {
            reticleTransform.position = input.reticlePosition;
        }
    }
}
