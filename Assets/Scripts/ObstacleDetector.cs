using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class ObstacleDetector : MonoBehaviour
{
    public LayerMask layer;

    public SplineContainer container;
    List<BezierKnot> knotsPos;
    private void GetKnotes()
    {
        knotsPos = container.Spline.ToList();
    }

    Vector3 startPoint;
    Vector3 endPoint;
   
    public bool CheckIfPathIsClear()
    {
        container = GetComponent<SplineContainer>();
        GetKnotes();
        bool thereIsACarOnThePath = false;
        for (int i = 0; i < container.Spline.Knots.Count() - 1 ; i++)
        {
            startPoint = knotsPos[i].Position;//transform.TransformPoint(knotsPos[i].Position);
            endPoint = knotsPos[i + 1].Position;//transform.TransformPoint(knotsPos[i + 1].Position);
            thereIsACarOnThePath = DetectCollition(startPoint, endPoint);
        }
        Debug.Log("Checking if there any car along the path" + thereIsACarOnThePath);
        return thereIsACarOnThePath;
    }
    private bool DetectCollition(Vector3 startPos, Vector3 endPos)
    {
        Vector3 direction = (endPos - startPos).normalized; // Calculate the direction from start to end
        float distance = Vector3.Distance(startPos, endPos); // Calculate the distance between start and end

        Debug.DrawLine(startPos, endPos, Color.black); // Draw the ray for visualization

        RaycastHit hit;
        if (Physics.Linecast(startPos, endPos, out hit)) // Perform the raycast
        {
            var controller = hit.collider.gameObject.GetComponent<VehicleController>();
            if (controller != null && controller.enabled) {
                 return false; 
            }
        }
        return true;
    }

    private void Update()
    {
        CheckIfPathIsClear();
        //CheckForVehicleObstacles();
    }
}
