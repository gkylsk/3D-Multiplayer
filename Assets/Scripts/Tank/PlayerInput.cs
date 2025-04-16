using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Camera Camera;

    private Vector3 reticlePosition;
    public Vector3 ReticlePosition
    {
        get { return reticlePosition; }
    }

    private Vector3 reticleNormal;
    public Vector3 ReticleNormal
    {
        get { return reticleNormal; }
    }

    private float forwardInput;
    public float ForwardInput
    {
        get { return forwardInput; }
    }

    private float rotationInput;
    public float RotationInput
    {
        get { return rotationInput; }
    }
    private void OnDrawGizmos()
    {
        // draw gizmos for graphical representation
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(reticlePosition, 0.5f);
    }
    void Update()
    {
        if(Camera)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray screemRay = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; // data information when hit something
        if(Physics.Raycast(screemRay, out hit))
        {
            reticlePosition = hit.point;
            reticleNormal = hit.normal;
        }

        forwardInput = Input.GetAxis("Vertical"); // Forward/backward input
        rotationInput = Input.GetAxis("Horizontal"); // Rotation input
    }
}
