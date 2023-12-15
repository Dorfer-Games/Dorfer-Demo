using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

public class SplineContext : EditorWindow
{
    [MenuItem("CONTEXT/SplineContainer/DuplicateSpline")]
    static void DoSomething(MenuCommand command)
    {
        for (int i = 0; i < 3; i++)
        {
            Duplicate(command);
        }
    }

    private static void Duplicate(MenuCommand command)
    {
        //var offsetScale = 2.12f;
        var offsetScale = 3.16f;
        var container = command.context as SplineContainer;
        var last = container.Splines.Count - 1;
        var spline = container[last];
        var knots = spline.Knots.ToArray();

        for (int i = 0; i < knots.Length; i++)
        {
            float3 offset = (Quaternion)knots[i].Rotation * (Vector3.left * offsetScale);
            knots[i].Position += offset;
            if (((Vector3)knots[i].TangentOut).magnitude != 0)
            {
                knots[i].TangentOut.z += offsetScale;
                knots[i].TangentIn.z -= offsetScale;
            }
        }
        var newSpline = new Spline(knots, true);
        container.AddSpline(newSpline);
    }
}
