using UnityEngine.UI;
using UnityEngine;

public class ClosePanelButton : MonoBehaviour
{
    Button button;
    [SerializeField] GameObject panelToClose;
    private void OnEnable()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(ClosePanel);
    }
    void ClosePanel()
    {
        UIManager.Instance.PanelFadeOut(panelToClose, 1f);
    }
}
