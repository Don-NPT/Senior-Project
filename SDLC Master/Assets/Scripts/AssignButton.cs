
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class AssignButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler 
{
    public string hoverSound = "Hover";
    public string clickSound = "Click";
    private StaffAssigner staffAssigner;
    public GameObject staffToAssign;

    private void Start() {
        staffAssigner = FindObjectOfType(typeof(StaffAssigner)) as StaffAssigner; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        FindObjectOfType<AudioManager>().Play(hoverSound);
    }

    public void OnPointerDown (PointerEventData eventData) 
     {
        FindObjectOfType<AudioManager>().Play(clickSound);
        staffAssigner.AssignStaff();
        transform.DOPunchScale (new Vector3 (0.2f, 0.2f, 0.2f), .25f);

        string staffName = transform.parent.GetComponentsInChildren<TextMeshProUGUI>()[0].text;
        GameObject[] allStaffs = GameObject.FindGameObjectsWithTag("Staff");
        foreach(var staff in allStaffs)
        {
            var name = staff.GetComponent<StaffProperties>().fname;
            if(name  == staffName)
                // staffToAssign = staff;
                GameManager.instance.staffToAssign = staff;
        }

        GameObject staffListUI =  GameObject.Find("/Canvas/StaffList");
        staffListUI.SetActive(false);
     }


}
