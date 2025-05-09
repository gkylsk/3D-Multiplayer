using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletHandler : NetworkBehaviour
{
    [SerializeField] float launchSpeed = 75f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ParticleSystem _particle;

    public TankBehaviour tankBehaviour;
    void Update()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.Play("Shoot");
            SpawnBullet();
            RPC_PlayParticleEffect();
        }
    }

    void SpawnBullet()
    {
        //Instantiate bullet
        NetworkObject bullet = Runner.Spawn(bulletPrefab, transform.position, transform.rotation);

        //direction in which bullet is fired 
        Vector3 localXDirection = transform.TransformDirection(Vector3.forward);

        Vector3 velocity = localXDirection * launchSpeed;

        bullet.GetComponent<Rigidbody>().velocity = velocity;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    private void RPC_PlayParticleEffect()
    {
        _particle.Play();
    }

}
