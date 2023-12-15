using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.Splines;

public class LevelComponent : MonoBehaviour
{
    [SerializeField] private CameraAnimator cameraAnimator;
    [field: SerializeField] public SplineContainer spline { get; private set; }
    [field: SerializeField] public TribuneDict tribines { get; private set; }

    [field: SerializeField] public CinemachineVirtualCamera startupCamera { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera mainCamera { get; private set; }

    public float splineLength => spline.CalculateLength(0);

    public void ShowMainCamera()
    {
        cameraAnimator.SetActiveCamera(true);
        mainCamera.enabled = true;
        startupCamera.enabled = false;
    }

    public void ShowStartupCamera()
    {
        cameraAnimator.SetActiveCamera(false);
        mainCamera.enabled = false;
        startupCamera.enabled = true;
    }

    public Point GetStartPoint(CarComponent car)
    {
        return new Point(
            spline.EvaluatePosition(car.index, 0),
            Quaternion.LookRotation(spline.EvaluateTangent(car.index, 0), Vector3.up)
        );
    }

    public Point GetCarPoint(CarComponent car)
    {
        var repeatedT = Mathf.Repeat(car.movementProgress, 1);
        return new Point(
            spline.EvaluatePosition(car.index, repeatedT),
            Quaternion.LookRotation(spline.EvaluateTangent(car.index, repeatedT), Vector3.up)
        );
    }

    [Serializable]
    public class TribuneDict : SerializableDictionary<CarDefinition, TribuneComponent>
    {
    }
}