using System;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    private static VehicleManager _instance;

    public static VehicleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VehicleManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("VehicleManager");
                    _instance = singletonObject.AddComponent<VehicleManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

       
        RefreshVehicleReferences();
    }

    public Vehicle[] vehicles;

    public GameObject GetCarType(Vehicles vehicle)
    {
        
        RefreshVehicleReferences();

        foreach (Vehicle car in vehicles)
        {
            if (car.CarType == vehicle)
            {
                GameObject obj = Instantiate(car.vehiclePrefab);
                return obj;
            }
        }
        return null;
    }

    private void RefreshVehicleReferences()
    {
        foreach (Vehicle car in vehicles)
        {
            if (car.vehiclePrefab == null)
            {
                
                string path = "Vehicles/" + car.CarType.ToString(); 
                car.vehiclePrefab = Resources.Load<GameObject>(path);

                if (car.vehiclePrefab == null)
                {
                    Debug.LogError($"Failed to load prefab for {car.CarType} from Resources folder.");
                }
            }
        }
    }

    public enum Vehicles
    {
        YellowCar,
        Police,
        Bus,
        Tocus
    }

    [Serializable]
    public class Vehicle
    {
        public Vehicles CarType;
        public GameObject vehiclePrefab;
    }
}
