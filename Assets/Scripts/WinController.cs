using UnityEngine;

public class WinController : MonoBehaviour
{
    public static WinController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public int totalCarsToFinish;
    private int finishedCars;

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
