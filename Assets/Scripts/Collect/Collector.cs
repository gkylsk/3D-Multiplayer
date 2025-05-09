using Fusion;
using UnityEngine;

public class Collector : NetworkBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(HasInputAuthority)
        {
            IItem item = collision.GetComponent<IItem>();
            item?.Collect();
        }
    }
}
