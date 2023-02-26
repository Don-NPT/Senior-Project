using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class StaffMiniItem : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public void OnPointerDown (PointerEventData eventData) 
    {
        // gameObject.SetActive(false);
        // FindObjectOfType<TeamManager>().SelectStaff(GetComponentsInChildren<TextMeshProUGUI>()[0].text.ToString());
    }
}
