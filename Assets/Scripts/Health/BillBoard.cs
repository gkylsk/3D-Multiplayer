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
        //always look towards the camera
        transform.LookAt(transform.position + cam.forward);
    }
}
