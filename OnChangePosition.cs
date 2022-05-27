using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
   public PolygonCollider2D hole2dColider;
   public PolygonCollider2D groundColider;

   public float initialScale = 0.5f;
   
   Mesh GeneratedMesh;
   public MeshCollider GeneratedMeshCollider;
    public float speed = 2;
    public float turnSmooth = 0.1f;
    float smoothvelocity;
    private Vector3 Direction;

   private void FixedUpdate()
       {
           if(transform.hasChanged == true)
           {
               transform.hasChanged = false;
               hole2dColider.transform.position = new Vector2(transform.position.x, transform.position.z);
               hole2dColider.transform.localScale = transform.localScale * initialScale;
               MakeHole2d();
               Make3dMeshCollider();

           }
       }

       void Update()
       {
           float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Direction = new Vector3(h, 0f, v).normalized;

        if (Direction.magnitude >= 0.1f)
        {
        float TargetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref smoothvelocity, turnSmooth);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

       }

       private void MakeHole2d()
       {
           Vector2[] PointPositons = hole2dColider.GetPath(0);

           for(int i = 0; i < PointPositons.Length; i++)
           {
               PointPositons[i] = hole2dColider.transform.TransformPoint(PointPositons[i]);
           }

           groundColider.pathCount = 2;
           groundColider.SetPath(1, PointPositons);
       }

       private void Make3dMeshCollider()
       {
           if(GeneratedMesh != null)
           {
               Destroy(GeneratedMesh);
           }
           GeneratedMesh = groundColider.CreateMesh(true, true);
           GeneratedMeshCollider.sharedMesh = GeneratedMesh;

       }
}
