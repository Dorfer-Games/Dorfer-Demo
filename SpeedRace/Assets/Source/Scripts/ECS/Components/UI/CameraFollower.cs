using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Start()
    {
        LookAtCamera();
    }

    private void Update()
    {
        LookAtCamera();
    }

    private void LateUpdate()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        var rotation = Quaternion.LookRotation(-_camera.forward, Vector3.up);
        transform.rotation = rotation;
        transform.Rotate(0, 180, 0);
    }
}
