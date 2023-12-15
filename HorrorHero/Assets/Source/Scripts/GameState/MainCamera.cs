using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Kuhpik;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MainCamera : GameSystem, IIniting, IUpdating
{
    [SerializeField] private Camera cam;

    private Quaternion rotate;

    void IIniting.OnInit()
    {
        rotate = cam.transform.rotation;
    }
    // Update is called once per frame
   void IUpdating.OnUpdate()
    {
        /// для прямых уровней
       Vector3 pos = cam.transform.position;
        pos.z = game.murder.transform.position.z - 7;
        cam.transform.position = pos;

        /*rotate.y = game.murder.transform.rotation.y;
        cam.transform.rotation = rotate; /**/
    }
}
