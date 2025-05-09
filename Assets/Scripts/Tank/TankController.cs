using Cinemachine;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NetworkTransform))]
public class TankController : NetworkBehaviour
{
    [SerializeField] private Text nameText;

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

    public override void Spawned()
    {
        //set up the virtual camera for the repective player
        if (Object.HasInputAuthority)
        {
            GameObject virtualCamera = GameObject.Find("Virtual Camera");
            if (virtualCamera != null)
            {
                virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = transform;
            }
        }
    }

    public override void FixedUpdateNetwork()
    {
        //tank and turret movement variables
        float forward;
        float rotation;
        Vector3 reticlePosition;
        Vector3 reticleNormal;

        //tank movement input
        forward = Input.GetAxis("Vertical"); // Forward/backward input
        rotation = Input.GetAxis("Horizontal"); // Rotation input

        //tank movement and wheel rotation
        HandleMovement(forward, rotation);
        HandleWheelRotation(forward,rotation);

        // Raycast for reticle
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            reticlePosition = hit.point;
            reticleNormal = hit.normal;
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
            HandleTurret(reticlePosition);
            //HandleReticle(reticlePostion);
        }
    }
    
    void HandleMovement(float forward, float rotation)
    {
        // Move the tank
        Vector3 forwardMovement = transform.forward * forward * tankSpeed * Runner.DeltaTime;
        transform.position += forwardMovement;

        // Rotate the tank
        transform.Rotate(Vector3.up, rotation * tankRotationSpeed * Runner.DeltaTime);
    }

    void HandleWheelRotation(float forward, float rotation)
    {
        float wheelRotation = forward * wheelRotationSpeed * Runner.DeltaTime;
        foreach (GameObject wheel in leftWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation * rotation * wheelRotationSpeed * Runner.DeltaTime, 0, 0);
            }
        }
        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
            {
                wheel.transform.Rotate(wheelRotation + rotation * wheelRotationSpeed * Runner.DeltaTime, 0, 0);
            }
        }
    }

    void HandleTurret(Vector3 reticlePosition)
    {
        if(turretTransform)
        {
            Vector3 turretLookDir = reticlePosition - turretTransform.position;
            turretLookDir.y = 0;
            finalTurretLookDir = Vector3.Lerp(finalTurretLookDir, turretLookDir, Runner.DeltaTime * turretLagSpeed);
            Rpc_SetTurretRotation(Quaternion.LookRotation(finalTurretLookDir));
        }
    }

    void HandleReticle(Vector3 reticlePosition)
    {
        if(reticleTransform)
        {
            reticleTransform.position = reticlePosition;
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void Rpc_SetTurretRotation(Quaternion rotation)
    {
        turretTransform.rotation = rotation;
    }

}
