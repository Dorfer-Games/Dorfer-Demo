using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using System;
using System.Diagnostics;

[System.Serializable]
public class Truck : System.Object
{
	public WheelCollider Wheel_L;
	public GameObject Wheel_L_Mesh;
	public WheelCollider Wheel_R;
	public GameObject Wheel_R_Mesh;
	public bool Motor;

	[Range(-90, 90)] public float SteeringAngle = 0;
}

public class Truck_Controller : MonoBehaviour 
{
	[HideInInspector] public float Moving;
	[HideInInspector] public float Steering;
	[HideInInspector] public float Brake;

	[SerializeField]Vector3 CenterOfMass;

	public float Torque = 500;
	public List<Truck> Trucks;
	
	//Temp
	Quaternion rot;
	Vector3 pos;

    private void Start()
    {
		gameObject.GetComponent<Rigidbody>().centerOfMass = CenterOfMass;
	}

    public void Update()
	{
		float motor = Torque * Moving;
		/*
		if (Input.GetKeyDown(KeyCode.W))
			Moving += 1;
		if (Input.GetKeyDown(KeyCode.S))
			Moving -= 1;
		if (Input.GetKeyUp(KeyCode.S))
			Moving += 1;
		if (Input.GetKeyUp(KeyCode.W))
			Moving -= 1;

		if (Input.GetKeyDown(KeyCode.D))
			Steering += 1;
		if (Input.GetKeyDown(KeyCode.A))
			Steering -= 1;
		if (Input.GetKeyUp(KeyCode.A))
			Steering += 1;
		if (Input.GetKeyUp(KeyCode.D))
			Steering -= 1;

		if (Input.GetKeyDown(KeyCode.Space))
			Brake = Torque;
		if (Input.GetKeyUp(KeyCode.Space))
			Brake = 0;
		*/
		float brakeTorque = Brake;
		if (brakeTorque > 0.001)		{
			brakeTorque = Torque;
			motor = 0;
		}
		else
			brakeTorque = 0;

		foreach (Truck CurrentTruck in Trucks)
		{
			CurrentTruck.Wheel_L.steerAngle = CurrentTruck.Wheel_R.steerAngle = CurrentTruck.SteeringAngle* Steering;

			if (CurrentTruck.Motor == true){
				CurrentTruck.Wheel_L.motorTorque = motor;
				CurrentTruck.Wheel_R.motorTorque = motor;
			}

			CurrentTruck.Wheel_L.brakeTorque = brakeTorque; 
			CurrentTruck.Wheel_R.brakeTorque = brakeTorque;

			if (CurrentTruck.Wheel_L_Mesh)
			{
				CurrentTruck.Wheel_L.GetWorldPose(out pos, out rot);
				CurrentTruck.Wheel_L_Mesh.transform.position = pos;
				CurrentTruck.Wheel_L_Mesh.transform.rotation = rot;
			}
            if (CurrentTruck.Wheel_R_Mesh)
            {
                CurrentTruck.Wheel_R.GetWorldPose(out pos, out rot);
                CurrentTruck.Wheel_R_Mesh.transform.position = pos;
                CurrentTruck.Wheel_R_Mesh.transform.rotation = rot;
            }
		}
	}
}