using System;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    public static event Action<int> OnCoinCollect;
    public int value;

    public void Collect()
    {
        OnCoinCollect.Invoke(value);
        Destroy(gameObject);
    }
}
