using UnityEngine.EventSystems;
using UnityEngine;

public class CommandBar : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public bool seeDetail = false;
    public bool assignStaff = false;
    // public bool assignWork = false;
    public bool openShop = false;
    public bool hireStaff = false;
    public bool sellProp = false;
    public Vector3 offsetY = new Vector3(0, 2.1f, 0);
    private StaffAssigner staffAssigner;
    private GameObject staffGO;
    private GameObject staffUIGO;
    private ShopOpener shopOpener;
    public PanelOpener panelOpener;

    private void Start() 
    {
        if(assignStaff) staffAssigner = FindObjectOfType(typeof(StaffAssigner)) as StaffAssigner; 
        if(openShop) shopOpener = FindObjectOfType(typeof(ShopOpener)) as ShopOpener; 
    }

    public void Setup(GameObject GO)
    {
        staffGO = GO;
        // GetComponentInParent<Transform>().position = Camera.main.WorldToScreenPoint(GO.transform.position + offsetY);
        // Debug.Log(GetComponentInParent<Transform>().position);
    }

    public void SetupWithUI(GameObject GO, GameObject ui)
    {
        staffGO = GO;
        staffUIGO = ui;
        // GetComponentInParent<Transform>().position = Camera.main.WorldToScreenPoint(GO.transform.position + offsetY);
        // Debug.Log(GetComponentInParent<Transform>().position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("Hover");
    }

    public void OnPointerDown (PointerEventData eventData) 
    {
        FindObjectOfType<AudioManager>().Play("Click");
        if(seeDetail) GameManager.instance.ShowPlayerDetail();
        if(assignStaff) staffAssigner.AssignStaff();
        // if(assignWork) staffGO.GetComponent<PlayerController>().AssignWork();
        if(openShop) ShopOpener.instance.OpenPanel();
        if(hireStaff) panelOpener.OpenPanel();
        if(sellProp) {
            GameManager.instance.AddMoney(staffGO.GetComponent<Prop>().price);
            Destroy(staffGO);
            Destroy(staffUIGO);
        }
    }
}
