using UnityEngine;
using DG.Tweening;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;
    public void OpenPanel()
    {
        if(panel != null)
        {
            panel.transform.localScale = Vector3.zero;
            panel.SetActive(true);
            panel.transform.DOScale(1, 0.3f).SetEase(Ease.OutQuad);
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
