using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public bool end;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Victim")
        {
            end = true;
        }
    }
}
