using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] Transform cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>().transform;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
