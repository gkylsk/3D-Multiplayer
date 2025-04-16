using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField] float launchSpeed = 75f;
    [SerializeField] GameObject bulletPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Fire1") == 1)
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        //Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        //direction in which bullet is fired 
        Vector3 localXDirection = transform.TransformDirection(Vector3.forward);

        Vector3 velocity = localXDirection * launchSpeed;

        bullet.GetComponent<Rigidbody>().velocity = velocity;
    }
}
