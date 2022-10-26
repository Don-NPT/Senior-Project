using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class StaffMemberItem : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public void OnPointerDown (PointerEventData eventData) 
    {
        FindObjectOfType<TeamManager>().UnselectStaff(GetComponentsInChildren<TextMeshProUGUI>()[0].text.ToString());
        Destroy(this.gameObject);
    }
}
