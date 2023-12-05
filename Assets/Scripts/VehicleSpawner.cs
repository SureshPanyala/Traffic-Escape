using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class VehicleSpawner : MonoBehaviour
{
    SplineContainer spline;
    [SerializeField] VehicleManager.Vehicles carType;
    [SerializeField] SymbolManager.SymbolsEnum symbolsType;

    GameObject car;
    private void OnEnable()
    {
        spline = GetComponent<SplineContainer>();
    }
    private void Start()
    {
       StartCoroutine(InstantiateVehicle());
    }
    IEnumerator InstantiateVehicle()
    {
        car = VehicleManager.Instance.GetCarType(vehicle: carType);
        car.transform.position = transform.position;

        AddSplineAnimateToCar();
        ApplySymbolOnCar();
        car.SetActive(true);
        yield return null;
    }

    private void AddSplineAnimateToCar()
    {
        SplineAnimate animate = car.AddComponent<SplineAnimate>();
        animate.PlayOnAwake = false;
        animate.Container = spline;
        animate.AnimationMethod = SplineAnimate.Method.Speed;
        animate.MaxSpeed = 15;
        animate.Loop = SplineAnimate.LoopMode.Once;
        //car.AddComponent<VehicleObstacleDetector>();
    }

    private void ApplySymbolOnCar()
    {
        if (Enum.IsDefined(typeof(SymbolManager.SymbolsEnum), symbolsType))
        {
            car.GetComponent<VehicleController>().directionMarkImage.sprite = SymbolManager.GetSymbol(symbolsType);
        }
    }
}