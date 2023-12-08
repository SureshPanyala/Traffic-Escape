using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PowerType currentPower = PowerType.none;
    public GameObject helicopterObject;
    public bool isCarMoving = false;
    public int helicopterCount=10;
    public int switchCount=10;

    public List<VehicleSpawner> vehicleSpawnerList;
    public List<ObstacleDetector> obstacleDetectorList;

    public GameObject healthObject;
    public bool isHelicopterModeOn = false;
    public bool isSwitchModeOn = false;
    public bool checkPowerMode = false;

    [SerializeField] public Button helicopterButton;
    [SerializeField] public Button switchButton;
    public void CheckEligibleVehiclesForSignChange()
    {
        for (int i = 0; i < vehicleSpawnerList.Count; i++)
        {
            if (vehicleSpawnerList[i].gameObject.activeInHierarchy)
            {
                vehicleSpawnerList[i].car.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }
    public void CheckEligibleVehiclesForSignback()
    {
        for (int i = 0; i < vehicleSpawnerList.Count; i++)
        {
            if (vehicleSpawnerList[i].gameObject.activeInHierarchy)
            {
                vehicleSpawnerList[i].car.gameObject.transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }

    public void GetCompleteCarListForHint()
    {
        for (int i = 0; i < obstacleDetectorList.Count; i++)
        {
            if (obstacleDetectorList[i].gameObject.activeInHierarchy)
            {
                if (obstacleDetectorList[i].CheckIfPathIsClear() == true)
                {
                    if (obstacleDetectorList[i].gameObject.GetComponent<VehicleSpawner>() != null)
                    {
                        obstacleDetectorList[i].gameObject.GetComponent<VehicleSpawner>().car.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    }
                    else
                    {
                            //Transform parentTransform = obstacleDetectorList[i].transform.parent;
                            //parentTransform.GetComponent<VehicleSpawner>().car.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    public void ResetHint()
    {
        for (int i = 0; i < obstacleDetectorList.Count; i++)
        {
            if (obstacleDetectorList[i].gameObject.activeInHierarchy)
            {
                if (obstacleDetectorList[i].gameObject.GetComponent<VehicleSpawner>() != null)
                {
                    obstacleDetectorList[i].gameObject.GetComponent<VehicleSpawner>().car.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                }
                else
                {

                }
            
            }
        }
    }
    public enum PowerType
    {
        helicopter,
        switchdirection,
        none,
        
    }
    // Start is called before the first frame update
    void Start()
    {
        checkPowerMode = false;
        //PlayerPrefs.DeleteAll();
        helicopterCount = PlayerPrefs.GetInt("HelicopterCount", 500);
        switchCount = PlayerPrefs.GetInt("SwitchCount", 500);
        currentPower = PowerType.none;
        instance = this;
    }
    public void SetSwitchDirection()
    {
        checkPowerMode = true;
        if (switchCount > 0)
        {
            isSwitchModeOn = true;
            CheckEligibleVehiclesForSignChange();
            currentPower = PowerType.switchdirection;
           
        }
    }
    public void SetHelicopter()
    {
        checkPowerMode = true;
        //foreach(GameObject x in SplineObjects)
        //{
        //    CarObjects.Add(x.gameObject.transform.GetComponent<VehicleSpawner>().car);
        //}
        if (helicopterCount > 0)
        {
               isHelicopterModeOn = true;
               currentPower = PowerType.helicopter;
            
        }
    }

    public void SwitchCountNumber()
    {
        isSwitchModeOn = false;
        switchCount--;
        
        PlayerPrefs.SetInt("SwitchCount", switchCount);
        PlayerPrefs.Save();
    }
    public void HelicopterCountNumber()
    {
        helicopterCount--;
        PlayerPrefs.SetInt("HelicopterCount", helicopterCount);
        PlayerPrefs.Save();
    }


    public void CallTakeDamage()
    {
        if (healthObject != null)
        {
            healthObject.GetComponent<HealthManager>().TakeDamage(5f);
        }
    }






    // Update is called once per frame
    void Update()
    {
        if (checkPowerMode == true)
        {
            if (isHelicopterModeOn == true || isSwitchModeOn == true)
            {
                helicopterButton.interactable = false;
                switchButton.interactable = false;

            }
            if (isHelicopterModeOn == false && isSwitchModeOn == false)
            {
                helicopterButton.interactable = true;
                switchButton.interactable = true;
            }
        }
    }

}
