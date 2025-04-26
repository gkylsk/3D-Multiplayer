using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public float forward;
    public float rotation;
    public Vector3 reticlePosition;
    public Vector3 reticleNormal;
}
