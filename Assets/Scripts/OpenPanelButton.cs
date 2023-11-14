using UnityEngine;
using UnityEngine.UI;

public class OpenPanelButton : MonoBehaviour
{
    Button button;
    [SerializeField] GameObject panelToOpen;
    private void OnEnable()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(OpenSettings);
    }
    void OpenSettings()
    {
        UIManager.Instance.PanelFadeIn(panelToOpen, 1);
    }
}
