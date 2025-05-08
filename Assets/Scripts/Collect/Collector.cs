using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : NetworkBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(HasInputAuthority);
        if(HasInputAuthority)
        {
            IItem item = collision.GetComponent<IItem>();
            item?.Collect();
        }
    }
}
