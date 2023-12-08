using System;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Splines;

public class VehicleSpawner : MonoBehaviour
{

    SplineContainer splineRoute;
    [SerializeField] VehicleManager.Vehicles carType;
    [SerializeField] SymbolManager.SymbolsEnum symbolsType;
    [SerializeField] Vector3 CarRotation;
    GameObject car;
    private void OnEnable()
    {
        splineRoute = GetComponent<SplineContainer>();
    }
    private void Start()
    {
       InstantiateVehicle();
    }
    private void InstantiateVehicle()
    {
        car = VehicleManager.Instance.GetCarType(vehicle: carType);
  
        AddSplineAnimateToCar();
        ApplySymbolOnCar();
        car.SetActive(true);

        car.GetComponent<VehicleController>().enabled = true;
<<<<<<< Updated upstream
       
=======
        yield return null;
    }
    public void SwitchSplineAnimateOfCar()
    {
        car.GetComponent<VehicleController>().directionMarkImage.sprite = SymbolManager.GetSymbol(ChangeSymbolsType);
        animate.Container = changeSplineRoute;
    }
    public void SwitchSpline()
    {
            animate.Container = changeSplineRoute;
            car.GetComponent<VehicleController>().directionMarkImage.sprite = SymbolManager.GetSymbol(ChangeSymbolsType);
            GameManager.instance.currentPower = GameManager.PowerType.none;
            GameManager.instance.isCarMoving = false;
>>>>>>> Stashed changes
    }

    private void AddSplineAnimateToCar()
    {
        SplineAnimate animate = car.AddComponent<SplineAnimate>();
        animate.PlayOnAwake = false;
        animate.Container = splineRoute;
        animate.AnimationMethod = SplineAnimate.Method.Speed;
        animate.MaxSpeed = 15;
        animate.Loop = SplineAnimate.LoopMode.Once;
        // car.transform.rotation = Quaternion.Inverse(splineRoute.Splines.ToList()[0].Knots.ToList()[0].Rotation);
        //Debug.Log("first knot rotaion is"+splineRoute.Splines.ToList()[0].Knots.ToList()[0].Rotation);
    }

    private void ApplySymbolOnCar()
    {
        if (Enum.IsDefined(typeof(SymbolManager.SymbolsEnum), symbolsType))
        {
            car.GetComponent<VehicleController>().directionMarkImage.sprite = SymbolManager.GetSymbol(symbolsType);
        }
    }

    private void Update()
    {
        if (car != null && splineRoute != null && splineRoute.Splines.Count > 0)
        {
          //  Quaternion initialRotation = Quaternion.Inverse(splineRoute.Splines.ToList()[0].Knots.ToList()[0].Rotation);
            car.transform.rotation = Quaternion.Euler(CarRotation);
        }
        // car.transform.localRotation = Quaternion.Euler(CarRotation);
    }
}