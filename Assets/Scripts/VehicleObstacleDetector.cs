using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Splines;

public class VehicleObstacleDetector : MonoBehaviour
{
    SplineAnimate anim;
    public LayerMask layer;

    List<BezierKnot> knots;
    private void Start()
    {
        anim = GetComponent<SplineAnimate>(); anim = GetComponent<SplineAnimate>();
        knots = anim.Container.Spline.Knots.ToList();
    }
    public bool CheckForVehicleObstacles()
    { 
        for (int i = 0; i < anim.Container.Spline.Knots.Count() - 1; i++)
        {
            Vector3 startPoint = (Vector3)(knots[i].Position);
            Vector3 endPoint = (Vector3)(knots[i + 1].Position);
            RaycastHit hit;
            Debug.Log("Start Point: " + startPoint);
            Debug.Log("End Point: " + endPoint);
           
            if (Physics.Linecast(startPoint, endPoint, out hit, layer))
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        CheckForVehicleObstacles();
    }
}
