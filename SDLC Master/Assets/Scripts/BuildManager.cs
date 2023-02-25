using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BuildManager : MonoBehaviour
{
    // private GameManager gameManager;
    public BuildPreset[] buildPreset;
    private GameObject prefab;
    private GameObject previewPrefab;
    private Mesh mesh;
    private Material mat;
    private Camera cam;
    private bool isSelected;
    public GameObject shopMenu;
    public GameObject closeButton;
    [SerializeField]
    private Ease customEase = Ease.Linear;
    private int buildIndex;
    public LayerMask mask;

    private GameObject preview;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected){
            DrawMesh();
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
            {
                PlaceObject();
            }
        }
    }

    void DrawMesh()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 500f, mask))
            {
                if(preview == null)
                    preview = (GameObject)Instantiate(previewPrefab, hit.point + new Vector3(0, 0, 0), Quaternion.identity);
                    
                preview.transform.position = hit.point;
                // Graphics.DrawMesh(mesh, hit.point, Quaternion.identity, mat, 0);
                if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject()) 
                {
                    preview.transform.eulerAngles += new Vector3(0, 90, 0);
                }
                // Debug.Log(preview.gameObject.GetComponent);
            }
    }

    void PlaceObject()
    {
        if(GameManager.instance.getMoney() >= buildPreset[buildIndex].price)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 500f, mask) && !preview.gameObject.GetComponentInChildren<BlueprintColiision>().isCollide())
            {
                GameObject buildObj = (GameObject)Instantiate(prefab, hit.point + new Vector3(0, 0, 0), preview.transform.rotation);
                buildObj.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                buildObj.transform.DOScale(1, 0.5f).SetEase(customEase);
                GameManager.instance.AddMoney(-1*buildPreset[buildIndex].price);
                FindObjectOfType<AudioManager>().Play("Purchase");
            }else{
                FindObjectOfType<AudioManager>().Play("Warning");
            }
        }else{
            FindObjectOfType<AudioManager>().Play("Warning");
        }
    }

    public void Select(int index)
    {
        Debug.Log("Select " + buildPreset[index].name);
        buildIndex = index;
        isSelected = true;
        shopMenu.SetActive(false);
        closeButton.SetActive(true);

        prefab = buildPreset[index].prefab;
        previewPrefab = buildPreset[index].previewPrefab;
        mesh = buildPreset[index].mesh;
        mat = buildPreset[index].material;

        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void UnSelect()
    {
        isSelected = false;
        closeButton.SetActive(false);
        Destroy(preview);
    }
}
