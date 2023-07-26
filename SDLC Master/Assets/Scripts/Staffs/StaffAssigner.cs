using UnityEngine.EventSystems;
using UnityEngine;

public class StaffAssigner : MonoBehaviour
{
    private bool staff_Selected;
    public GameObject staffPrefab;
    public GameObject staffPreviewPrefab;
    public GameObject PCsetPrefab;
    public GameObject PCsetPreviewPrefab;
    public Material[] shirtMats;
    private GameObject[] PCsets;
    private GameObject preview;

    public Vector3 yOffset = new Vector3(0, 0.1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        staff_Selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(staff_Selected);
        if(staff_Selected)
        {
            DrawMesh();
            // HighLightDesk();
            CheckClick();
        }
    }

    public void AssignStaff(){
        staff_Selected = true;
    }

    void DrawMesh()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
             {
                GameObject obj = hit.transform.gameObject;
                if (obj.CompareTag("Chair"))
                {
                    if(preview == null)
                    {
                        var rotationVector = obj.transform.rotation.eulerAngles;
                        rotationVector.y += 180;
                        preview = (GameObject)Instantiate(staffPreviewPrefab, obj.transform.position+yOffset, Quaternion.Euler(rotationVector));
                    }
                }
                else{
                    if(preview != null) Destroy(preview);
                }
             } 
    }

    private void CheckClick(){
        if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit = new RaycastHit();      
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
 
             if (Physics.Raycast(ray, out hit))
             {
                GameObject obj = hit.transform.gameObject;
                if (obj.CompareTag("Chair"))
                {
                    var rotationVector = obj.transform.rotation.eulerAngles;
                    rotationVector.y += 180;
                    GameObject staff = GameManager.instance.staffToAssign;
                    Debug.Log(staff.GetComponent<StaffProperties>().fname);
                    staff.transform.position = obj.transform.position+yOffset;
                    staff.transform.rotation = Quaternion.Euler(rotationVector);
                    staff.GetComponent<StaffProperties>().isAssign = true;
                    staff.GetComponentsInChildren<SkinnedMeshRenderer>()[2].material = GetShirtColor(staff.GetComponent<StaffProperties>().position);
                    staff_Selected = false;
                    if(preview != null) Destroy(preview);
                }
             }     
        }
    }

    void HighLightDesk()
    {
        PCsets = GameObject.FindGameObjectsWithTag("PCset");

        GameObject[] PCsetPreview = new GameObject[PCsets.Length];
            for (int i=0;i<PCsets.Length;i++)
            {
                PCsetPreview[i] = Instantiate(PCsetPreviewPrefab, PCsets[i].transform.position, PCsets[i].transform.rotation);
                PCsets[i].SetActive(false);
            }
            Debug.Log(PCsets.Length);
    }

    public Material GetShirtColor(string position){
        switch(position)
            {
                case "Analyst":
                    return shirtMats[0];
                case "Designer":
                    return shirtMats[1];
                case "Programmer":
                    return shirtMats[2];
                case "Tester":
                    return shirtMats[3];
            }
        return null;
    }
}
