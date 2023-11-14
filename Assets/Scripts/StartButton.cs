using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(LoadUnlocedLevel);
    }

    void LoadUnlocedLevel()
    {
        LevelManager.LoadLevel(LevelManager.unlockedLevel);
    }
}
