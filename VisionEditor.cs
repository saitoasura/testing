using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(Vision))]
public class VisionEditor : Editor
{
    // Start is called before the first frame update
    void OnSceneGUI()
    {
     Vision fow = (Vision)target;
     //FOV 1
     Handles.color = Color.yellow;
     Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
     Vector3 viewAngleA = fow.DirfromAngle(-fow.viewAngle / 2, false);
     Vector3 viewAngleB = fow.DirfromAngle(fow.viewAngle / 2, false);
     Handles.color = Color.yellow;
     Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
     Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
     //FOV 2
     Handles.color = Color.red;
     Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius2);
     Vector3 viewAngle2A = fow.DirfromAngle(-fow.viewAngle2 / 2, false);
     Vector3 viewAngle2B = fow.DirfromAngle(fow.viewAngle2 / 2, false);
     Handles.color = Color.red;
     Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngle2A * fow.viewRadius2);
     Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngle2B * fow.viewRadius2);

     Handles.color = Color.yellow;
     foreach(Transform visibleTarget in fow.visibleTargets)
     {
         Handles.DrawLine(fow.transform.position, visibleTarget.position);
     }
     Handles.color = Color.red;
     foreach(Transform visibleTarget in fow.visibleTargets2)
     {
         Handles.DrawLine(fow.transform.position, visibleTarget.position);
     }
    }
}
