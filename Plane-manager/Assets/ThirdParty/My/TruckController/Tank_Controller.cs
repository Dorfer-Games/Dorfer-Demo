using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using System;
using System.Diagnostics;

public class Tank_Controller : MonoBehaviour
{
    public float movingL;
    public float movingR;
    [SerializeField] float ForcePower = 1f;
    [SerializeField] Vector3 CenterOfMass;

    [SerializeField] Transform LeftTrack;
    [SerializeField] Transform RightTrack;
    //Temp
    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().centerOfMass = CenterOfMass;
    }

    public void Update()
    {
        PlayerControl();
        Vector3 way = transform.forward;
        way.y = 0;
        rigid.AddForceAtPosition(way * ForcePower * movingL, LeftTrack.position, ForceMode.Force);
        rigid.AddForceAtPosition(way * ForcePower * movingR, RightTrack.position, ForceMode.Force);

        //rigid.AddRelativeTorque()
        //     Motors(motorL, WheelsL, WheelsModelL, brakeTorque);
        //     Motors(motorR, WheelsR, WheelsModelR, brakeTorque);
    }

    private void PlayerControl()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            movingL = Mathf.Clamp(movingL + 0.25f,-1,1);
        if (Input.GetKeyDown(KeyCode.A))
            movingL = Mathf.Clamp(movingL - 0.25f, -1, 1);
        if (Input.GetKeyDown(KeyCode.E))
            movingR = Mathf.Clamp(movingR + 0.25f, -1, 1);
        if (Input.GetKeyDown(KeyCode.D))
            movingR = Mathf.Clamp(movingR - 0.25f, -1, 1);
    }
}