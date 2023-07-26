using UnityEngine;
using DG.Tweening;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;

    private void Update() {
        if(Input.GetButtonDown("Cancel"))
        {
            ClosePanel();
        }
    }

    public void OpenPanelPunch()
    {
        if(panel != null)
        {
            panel.transform.localScale = Vector3.zero;
            panel.SetActive(true);
            panel.transform.DOScale(1, 0.3f).SetEase(Ease.OutQuad);
            GameManager.instance.panelOpen = true;
        }
    }

    public void OpenPanel()
    {
        if(panel != null)
        {
            panel.SetActive(true);
            GameManager.instance.panelOpen = true;
        }
    }

    public void ClosePanel()
    {
        if(panel != null)
        {
            panel.SetActive(false);
        }
    }
}
