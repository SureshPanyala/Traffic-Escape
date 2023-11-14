using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int unlockedLevel;
    private void Awake()
    {
        unlockedLevel = PlayerPrefs.GetInt("LEVEL", 1);
    }

    public static void LoadLevel(int level)
    {
        GameObject obj = Resources.Load<GameObject>("UI/LoadingCanvas");
        GameObject instance = Instantiate(obj);
        instance.GetComponent<LoadingCanvas>().LoadLevel(level);
    }

    public static void NewLevelUnlocked()
    {
        unlockedLevel++;
        SaveUnlockedLevel();
    }
    private static void SaveUnlockedLevel()
    {
        PlayerPrefs.SetInt("LEVEL", unlockedLevel);
    }
    public void LoadUnlockedLevel()
    {
        LoadLevel(unlockedLevel);
    }
    public void LoadMainMenu()
    {
        LoadLevel(0);
    }

    public static int CurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
