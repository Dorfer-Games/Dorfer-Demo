using UnityEngine;

public static class Vector3Helper
{
    public static Vector3 GetRandomVector3()
    {
        return new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
    }

    public static Vector3 GetRanomVector3WithSeed(int seed)
    {
        var tempRNGState = Random.state;
        Random.InitState(seed);
        var result = new Vector3(
            Random.Range(-1, 1f),
            Random.Range(-1, 1f),
            Random.Range(-1, 1f))
            .normalized;
        Random.state = tempRNGState;
        return result;
    }

    public static Vector3 GetRandomVector3XZ()
    {
        return new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, 1f)).normalized;
    }

    public static Vector3 GetRandomVector3XZ(int seed)
    {
        var tempRNGState = Random.state;
        Random.InitState(seed);
        var result = new Vector3(
            Random.Range(-1, 1f),
            0,
            Random.Range(-1, 1f))
            .normalized;
        Random.state = tempRNGState;
        return result;
    }

    public static Quaternion GetScreenRotationFromGlobalDirection(Vector3 playerPosition, Vector3 targetPosition, Camera camera)
    {
        targetPosition = playerPosition + (targetPosition - playerPosition).normalized;
        Vector2 screenDirection = camera.WorldToScreenPoint(targetPosition) - camera.WorldToScreenPoint(playerPosition);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, screenDirection.normalized);

        return rotation;
    }
}