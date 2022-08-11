using UnityEngine.EventSystems;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    private bool staff_Selected;
    public GameObject staffPrefab;
    public GameObject staffPreviewPrefab;
    public GameObject PCsetPrefab;
    private GameObject[] PCsets;
    private GameObject preview;
    public LayerMask mask;
    public Material highlightMat;
    private Material defaultMat;

    public Vector3 yOffset = new Vector3(0, 0.4f, 0);

    // Start is called before the first frame update
    void Start()
    {
        staff_Selected = false;
        defaultMat = GameObject.FindGameObjectsWithTag("PCset")[0].gameObject.GetComponentInChildren<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(staff_Selected);
        if(staff_Selected)
        {
            DrawMesh();
            HighLightDesk();
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
                    GameObject staff = (GameObject)Instantiate(staffPrefab, obj.transform.position+yOffset, Quaternion.Euler(rotationVector));
                    staff_Selected = false;
                    if(preview != null) Destroy(preview);
                    StopHighLight();
                }
             }     
        }
    }

    void HighLightDesk()
    {
        PCsets = GameObject.FindGameObjectsWithTag("PCset");
        foreach(GameObject Pcset in PCsets)
        {
            Renderer[] rends = Pcset.gameObject.GetComponentsInChildren<Renderer>();
            foreach(Renderer rend in rends)
            {
                Material[] mats = new Material[rend.materials.Length];
                for(int j=0; j<rend.materials.Length;j++)
                {
                    mats[j] = highlightMat;
                }
                rend.materials = mats;
            }
        }
    }

    void StopHighLight()
    {
        PCsets = GameObject.FindGameObjectsWithTag("PCset");
        foreach(GameObject Pcset in PCsets)
        {
            Renderer[] rends = Pcset.gameObject.GetComponentsInChildren<Renderer>();
            foreach(Renderer rend in rends)
            {
                Material[] mats = new Material[rend.materials.Length];
                for(int j=0; j<rend.materials.Length;j++)
                {
                    mats[j] = defaultMat;
                }
                rend.materials = mats;
            }
        }
    }
}
