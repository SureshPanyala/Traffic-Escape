using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Splines;

public class VehicleSpawner : MonoBehaviour
{

    [SerializeField] SplineContainer splineRoute;
    public SplineContainer changeSplineRoute;
    [SerializeField] VehicleManager.Vehicles carType;
    [SerializeField] SymbolManager.SymbolsEnum symbolsType;
    [SerializeField] SymbolManager.SymbolsEnum ChangeSymbolsType;
    [SerializeField] Vector3 CarRotation;
    public GameObject car;
    SplineAnimate animate;
    private void OnEnable()
    {
        splineRoute = GetComponent<SplineContainer>();
    }
    private void Start()
    {
      StartCoroutine(InstantiateVehicle());
    }
    private IEnumerator InstantiateVehicle()
    {
        car = VehicleManager.Instance.GetCarType(vehicle: carType);
  
        AddSplineAnimateToCar();
        ApplySymbolOnCar();
        car.SetActive(true);
       // car.transform.localRotation = transform.localRotation;
        car.GetComponent<VehicleController>().enabled = true;
        yield return null;
    }
    public void SwitchSplineAnimateOfCar()
    {
        car.GetComponent<VehicleController>().directionMarkImage.GetComponent<SpriteRenderer>().sprite = SymbolManager.GetSymbol(ChangeSymbolsType);
        animate.Container = changeSplineRoute;
    }
    public void SwitchSpline()
    {
            animate.Container = changeSplineRoute;
            car.GetComponent<VehicleController>().directionMarkImage.GetComponent<SpriteRenderer>().sprite = SymbolManager.GetSymbol(ChangeSymbolsType);
            GameManager.instance.currentPower = GameManager.PowerType.none;
    }
    private void AddSplineAnimateToCar()
    {
        animate = car.AddComponent<SplineAnimate>();
        animate.PlayOnAwake = false;
        animate.Container = splineRoute;
        animate.AnimationMethod = SplineAnimate.Method.Speed;
        animate.MaxSpeed = 15;
        animate.Loop = SplineAnimate.LoopMode.Once;
        animate.StartOffset = 0.001f;        // car.transform.rotation = Quaternion.Inverse(splineRoute.Splines.ToList()[0].Knots.ToList()[0].Rotation);
        //Debug.Log("first knot rotaion is"+splineRoute.Splines.ToList()[0].Knots.ToList()[0].Rotation);
    }

    private void ApplySymbolOnCar()
    {
        if (Enum.IsDefined(typeof(SymbolManager.SymbolsEnum), symbolsType))
        {
            car.GetComponent<VehicleController>().directionMarkImage.GetComponent<SpriteRenderer>().sprite = SymbolManager.GetSymbol(symbolsType);
        }
    }

    private void Update()
    {
        //if (car != null && splineRoute != null && splineRoute.Splines.Count > 0)
        //{
        //  //  Quaternion initialRotation = Quaternion.Inverse(splineRoute.Splines.ToList()[0].Knots.ToList()[0].Rotation);
           
        //}
    }
    private void FixedUpdate()
    {
      //  car.transform.localRotation = Quaternion.Euler(CarRotation);
    }
}