using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour
{
    public static LoadingCanvas instance;
    private void Awake()
    {
        instance = this;
    }
    public Image fillImage;
    public void LoadLevel(int levelidx)
    {
        StartCoroutine(FillProgressBar(levelidx));
    }
    public IEnumerator FillProgressBar(int idx)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(idx);
        while (!operation.isDone)
        {
            Debug.Log("Loading");
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            fillImage.fillAmount = progress;
            yield return null;
        }
        Debug.Log("Loading Completed");
        Destroy(gameObject);
    }
}
