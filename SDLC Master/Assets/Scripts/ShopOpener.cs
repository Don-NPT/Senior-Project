using DG.Tweening;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    public static ShopOpener instance;
    public GameObject ShopMenu;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OpenPanel()
    {
        
        if(ShopMenu != null)
        {

            ShopMenu.transform.localScale = Vector3.zero;
            ShopMenu.SetActive(true);
            ShopMenu.transform.DOScale(1, 0.3f).SetEase(Ease.OutQuad);
            
        }
    }

    public void ClosePanel()
    {
        if(ShopMenu != null)
        {
            ShopMenu.SetActive(false);
        }
    }
}
