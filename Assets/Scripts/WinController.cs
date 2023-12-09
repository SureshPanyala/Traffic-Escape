using UnityEngine;

public class WinController : MonoBehaviour
{
    public static WinController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private int totalCarsToFinish;
    private int finishedCars;
    private void Start()
    {
        finishedCars = 0;
        totalCarsToFinish = FindObjectsOfType<VehicleSpawner>().Length;
    }
    public int FinishedCars
    {
        get { return finishedCars; }
        set
        {
            finishedCars = value;
            CheckWinCondition();
        }
    }

    private void CheckWinCondition()
    {
        Debug.Log("NUM FINISHED CARS" + finishedCars);
        Debug.Log("TOTAL CARS TO FINISH" + totalCarsToFinish);
        if (finishedCars >= totalCarsToFinish)
        {
            SoundManager.instance.PlaySound(sound: SoundManager.Sounds.PartPop);
            ConfetteEffect.Instance.PlayConfetteEffect();
            UIManager.Instance.PanelFadeIn(UIManager.Instance.winPanel, 1);
            StarsManager.instance.DisplayStars(3);
            LevelManager.NewLevelUnlocked();
        }
    }
}
