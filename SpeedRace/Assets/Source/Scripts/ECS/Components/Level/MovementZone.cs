using System;
using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovementZone : GameSystem
{
    [SerializeField] private Transform center;
    [SerializeField] private Vector3 radius;

    public override void OnInit()
    {
        game.MovementZone = this;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(center.position, new Vector3(radius.x, 1, radius.z));
    }

    public Vector3 GetRandomPosition(float y)
    {
        var randomX = Random.Range(-radius.x / 2f, radius.x / 2f);
        var randomZ = Random.Range(-radius.z / 2f, radius.z / 2f);
        var randomPosition = center.position + new Vector3(randomX, y, randomZ);
        return randomPosition;
    }
}