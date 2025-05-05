using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] ParticleSystem _particle;

    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem ps = Instantiate(_particle, transform.position, Quaternion.identity);
        ps.Play();
        StartCoroutine(WaitForParticle());
    }
    IEnumerator WaitForParticle()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
