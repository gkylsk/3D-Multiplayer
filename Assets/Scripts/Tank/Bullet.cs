using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
