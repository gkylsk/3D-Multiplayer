using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    float yBound = 0;

    void Update()
    {
        if(transform.position.y < yBound)
        {
            Destroy(gameObject);
        }
    }
}
