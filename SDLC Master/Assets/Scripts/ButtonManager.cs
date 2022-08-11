
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler 
{
    public bool isButton = true;
    public string hoverSound = "Hover";
    public string clickSound = "Click";
    public bool assign = false;
    public bool doPunch = false;
    private StaffManager staffManager;

    private void Start() {
        if(assign) staffManager = FindObjectOfType(typeof(StaffManager)) as StaffManager; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if(isButton) FindObjectOfType<AudioManager>().Play(hoverSound);
    }

    public void OnPointerDown (PointerEventData eventData) 
     {
        if(isButton) FindObjectOfType<AudioManager>().Play(clickSound);
        if(assign) staffManager.AssignStaff();
        if(doPunch) transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .25f);
     }


}
