using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murder : MonoBehaviour
{
   
    private bool colission;
    private bool rotate;
    private Transform town;
    private List<GameObject> colisionTpuched;

    private void Start()
    {
        colission = false;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.tag.Contains("Floor"))
        {
            colission = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Rotate"))
        {
            rotate = true;
            Debug.Log("!!!");
        }

        if (other.gameObject.tag.Contains("Town"))
        {
            town = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Rotate"))
        {
            rotate = false;
            Debug.Log("...");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.tag.Contains("Floor"))
        {
            colission = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.tag.Contains("Floor"))
        {
            colission = false;
        }
    }

    public bool colissionBool()
    {
        return colission;
    }

    public bool RotateBool()
    {
        return rotate;
    }

    public Transform GetTown()
    {
        return town;
    }
}
