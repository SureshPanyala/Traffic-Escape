using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PowerType currentPower = PowerType.none;
    public GameObject helicopterObject;
    public bool isCarMoving = false;
    public int helicopterCount=10;
    public int switchCount=10;
    
    public enum PowerType
    {
        helicopter,
        switchdirection,
        none,
        
    }
    // Start is called before the first frame update
    void Start()
    {
        helicopterCount = PlayerPrefs.GetInt("HelicopterCount", 10);
        switchCount = PlayerPrefs.GetInt("SwitchCount", 10);
        currentPower = PowerType.none;
        instance = this;
    }
    public void SetSwitchDirection()
    {
        if (switchCount > 0)
        {
            currentPower = PowerType.switchdirection;
            switchCount--;
            PlayerPrefs.SetInt("SwitchCount", switchCount);
            PlayerPrefs.Save();
        }
    }
    public void SetHelicopter()
    {
        if (helicopterCount > 0)
        {
            currentPower = PowerType.helicopter;
            helicopterCount--;
            PlayerPrefs.SetInt("HelicopterCount", helicopterCount);
            PlayerPrefs.Save();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
