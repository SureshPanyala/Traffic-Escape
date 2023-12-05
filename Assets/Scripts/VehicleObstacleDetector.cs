using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class VehicleObstacleDetector : MonoBehaviour
{
    public LayerMask layer;

    public SplineContainer container;
    List<BezierKnot> knotsPos;
    private void Start()
    {
        container = GetComponent<SplineContainer>();
        GetKnotes();
    }
    private void GetKnotes()
    {
        knotsPos = container.Spline.ToList();
    }

    Vector3 startPoint;
    Vector3 endPoint;
    public bool CheckForVehicleObstacles()
    {
        bool canMOve = true;
        for (int i = 0; i < container.Spline.Knots.Count() - 1 ; i++)
        {
            if(i == 0)
            {
                startPoint = knotsPos[i].Position - (float3)transform.position;
            }
            else
            {
                startPoint = knotsPos[i].Position;//transform.TransformPoint(knotsPos[i].Position);
                endPoint = knotsPos[i + 1].Position;//transform.TransformPoint(knotsPos[i + 1].Position);
                canMOve = DetectCollition(startPoint, endPoint);
            }  
        }
        return canMOve;
    }
    private bool DetectCollition(Vector3 startPos, Vector3 endPos)
    {
        Vector3 direction = (endPos - startPos).normalized; // Calculate the direction from start to end
        float distance = Vector3.Distance(startPos, endPos); // Calculate the distance between start and end

        Debug.DrawRay(startPos, direction * distance, Color.black); // Draw the ray for visualization

        RaycastHit hit;
        if (Physics.Raycast(startPos, direction, out hit, distance, layer)) // Perform the raycast
        {
            return true;
        }
        else
            return false;
    }

    //private void Update()
    //{
    //    CheckForVehicleObstacles();
    //}
}
