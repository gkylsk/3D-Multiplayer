using System;
using System.Collections;
using UnityEngine;

public class HealthItem : MonoBehaviour, IItem
{
    public static event Action<int> OnHealthCollect;
    public int health;
    public float waitTime = 2f;
    public void Collect()
    {
        OnHealthCollect.Invoke(health);
        StartCoroutine(HealthCoroutine());
    }

    IEnumerator HealthCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        Collect();
    }
}
