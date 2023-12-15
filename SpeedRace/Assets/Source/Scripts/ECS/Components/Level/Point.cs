using System;
using UnityEngine;

[Serializable]
public struct Point
{
    public Vector3 position;
    public Quaternion rotation;

    public Point(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}