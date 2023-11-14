using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject winPanel;
    public GameObject GamePanel;
    public GameObject SettingsPanel;
    public GameObject touchTutorial;
    public void PanelFadeIn(GameObject panel, float fadetime)
    {
        CanvasGroup grp = panel.gameObject.GetComponent<CanvasGroup>();
        RectTransform rect = panel.gameObject.GetComponent<RectTransform>();
        panel.SetActive(true);
        grp.alpha = 0f;
        rect.transform.localPosition = new Vector3(0f, -1000f, 0f);
        rect.DOAnchorPos(new Vector2(0f, 0f), fadetime, false).SetEase(Ease.OutElastic);
        grp.DOFade(1, fadetime);
    }

    public void PanelFadeOut(GameObject panel, float fadetime)
    {
        CanvasGroup grp = panel.gameObject.GetComponent<CanvasGroup>();
        RectTransform rect = panel.gameObject.GetComponent<RectTransform>();
        grp.alpha = 1f;
        rect.transform.localPosition = new Vector3(0f, 0, 0f);
        rect.DOAnchorPos(new Vector2(0f, -1000f), fadetime, false).SetEase(Ease.InOutQuint);
        grp.DOFade(1, fadetime);
        StartCoroutine(DisablePanel(panel));
    }
    IEnumerator DisablePanel(GameObject panel)
    {
        yield return new WaitForSeconds(0.3f);
        panel.SetActive(false);
    }
}
