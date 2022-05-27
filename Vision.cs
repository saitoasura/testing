using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Vision : MonoBehaviour
{
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public LayerMask  allyMask;

    public LineRenderer LineRenderer;
    public LineRenderer LineRendererB;
    public LineRenderer LineRenderer2;
    public LineRenderer LineRenderer2B;
    [Header("FOV 1")]
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> allyvisibleTargets = new List<Transform>();

    [Header("FOV 2")]
    public float viewRadius2;
    [Range(0,360)]
    public float viewAngle2;

    public List<Transform> visibleTargets2 = new List<Transform>();
//LINE
    [SerializeField] private Vector3[] points;
    [SerializeField] private Vector3[] points2;
    [SerializeField] private Vector3[] points3;
    [SerializeField] private Vector3[] points4;
//ENDLINE

    void Start()
    {
     StartCoroutine(FindTargetWithDelay(0.2f));
     wanderRadius = 10;
     wanderTimer = 5;

     agent = GetComponent<NavMeshAgent> ();
     timer = wanderTimer;
    }

    void Update()
    {
         drawLine();
         
         /////WANDER AI

         timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    void drawLine(){

     for(int i = 0; i < points.Length; i++)
        {
            LineRenderer.SetPosition(i, points[i]);
        }
        for(int i = 0; i < points2.Length; i++)
        {
            LineRendererB.SetPosition(i, points2[i]);
        }
        for(int i = 0; i < points3.Length; i++)
        {
            LineRenderer2.SetPosition(i, points3[i]);
        }
        for(int i = 0; i < points4.Length; i++)
        {
            LineRenderer2B.SetPosition(i, points4[i]);
        }

     Vector3 viewAngleA = DirfromAngle(-viewAngle / 2, false);
     Vector3 viewAngleB = DirfromAngle(viewAngle / 2, false);
     Vector3 viewAngle2A = DirfromAngle(-viewAngle2 / 2, false);
     Vector3 viewAngle2B = DirfromAngle(viewAngle2 / 2, false);

     points[0] = gameObject.transform.position;
     points[1] = gameObject.transform.position + viewAngleA * viewRadius;
     points2[0] = gameObject.transform.position;
     points2[1] = gameObject.transform.position + viewAngleB * viewRadius;

     points3[0] = gameObject.transform.position;
     points3[1] = gameObject.transform.position + viewAngle2A * viewRadius2;
     points4[0] = gameObject.transform.position;
     points4[1] = gameObject.transform.position + viewAngle2B * viewRadius2;
     
     setupLine(points, points2, points3, points4);

    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
    visibleTargets.Clear();
    visibleTargets2.Clear();
    allyvisibleTargets.Clear();


      Collider[] allytargetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, allyMask);
        for(int i = 0; i < allytargetsInViewRadius.Length; i++)
        {
            Transform target = allytargetsInViewRadius[i].transform;
            allyvisibleTargets.Add(target);

        }

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }

        Collider[] targetsInViewRadius2 = Physics.OverlapSphere(transform.position, viewRadius2, targetMask);
        for(int i = 0; i < targetsInViewRadius2.Length; i++)
        {
            Transform target = targetsInViewRadius2[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle2 / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets2.Add(target);
                }
            }
        }

     if(visibleTargets2.Count > 0)
     {
         visibleTargets.Clear();
         this.gameObject.GetComponent<Renderer>().material.color = Color.red;
     }

     if(visibleTargets.Count > 0)
     {
         this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
     }


     if(visibleTargets.Count > 0 || visibleTargets2.Count > 0)
     {
         StartCoroutine(Warn());
     }

    }

    IEnumerator Warn()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < allyvisibleTargets.Count; i++)
        {
        allyvisibleTargets[i].gameObject.GetComponent<Renderer>().material.color = this.gameObject.GetComponent<Renderer>().material.color;
        }
        yield return new WaitForSeconds(5f);
        if (visibleTargets.Count == 0 && visibleTargets2.Count == 0)
         {
         for(int i = 0; i < allyvisibleTargets.Count; i++)
        {
        allyvisibleTargets[i].gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
         }
                    
    }

    

    public Vector3 DirfromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void setupLine(Vector3[] points,Vector3[] points2, Vector3[] points3, Vector3[] points4)
    {
        LineRenderer.positionCount = points.Length;
        this.points = points;
        LineRenderer.startColor = Color.yellow;
        LineRenderer.endColor = Color.yellow;
        LineRenderer.startWidth = 0.2f;
        LineRenderer.endWidth = 0.2f;

        LineRendererB.positionCount = points2.Length;
        this.points2 = points2;
        LineRendererB.startColor = Color.yellow;
        LineRendererB.endColor = Color.yellow;
        LineRendererB.startWidth = 0.2f;
        LineRendererB.endWidth = 0.2f;

        LineRenderer2.positionCount = points3.Length;
        this.points3 = points3;
        LineRenderer2.startColor = Color.red;
        LineRenderer2.endColor = Color.red;
        LineRenderer2.startWidth = 0.2f;
        LineRenderer2.endWidth = 0.2f;

        LineRenderer2B.positionCount = points4.Length;
        this.points4 = points4;
        LineRenderer2B.startColor = Color.red;
        LineRenderer2B.endColor = Color.red;
        LineRenderer2B.startWidth = 0.2f;
        LineRenderer2B.endWidth = 0.2f;
    }

    public float wanderRadius = 20;
    public float wanderTimer;
 
    private Transform target;
    private NavMeshAgent agent;
    private float timer;
 
 
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}
