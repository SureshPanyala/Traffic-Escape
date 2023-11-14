using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField] Button skipLevelButton;
    [SerializeField] Button settingsButton;

    [SerializeField] TextMeshProUGUI levelInfoText;
    [SerializeField] TextMeshProUGUI movesLeftText;

    private void Start()
    {
        SetLevelInfo();
    }

    private void SetLevelInfo()
    {
        int levelIdx = LevelManager.CurrentLevel();
        string txt = $"Level {levelIdx}";
        levelInfoText.text = txt;
    }
}
