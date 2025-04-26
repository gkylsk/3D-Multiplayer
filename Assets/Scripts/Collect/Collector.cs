using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        IItem item = collision.GetComponent<IItem>();
        item?.Collect();
    }
}
