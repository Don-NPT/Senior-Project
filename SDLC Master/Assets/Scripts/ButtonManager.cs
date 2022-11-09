
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
    public bool isTabButton = false;
    private StaffAssigner staffAssigner;
    public bool isSelected = false;

    private void Start() {
        if(assign) staffAssigner = FindObjectOfType(typeof(StaffAssigner)) as StaffAssigner; 
        if(isTabButton) isSelected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if(isButton) FindObjectOfType<AudioManager>().Play(hoverSound);
    }

    public void OnPointerDown (PointerEventData eventData) 
     {
        if(isButton) FindObjectOfType<AudioManager>().Play(clickSound);
        if(assign) staffAssigner.AssignStaff();
        if(doPunch) transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .25f);
        if(isTabButton) isSelected = true;
     }

     public void OnDisable()
     {
        if(isTabButton) isSelected = false;
     }


}
