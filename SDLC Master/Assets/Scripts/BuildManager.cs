using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class BuildManager : MonoBehaviour
{
    // private GameManager gameManager;
    public static BuildManager instance;
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
    public List<BuildObjData> buildObjDataList = new List<BuildObjData>();

    private void Awake() {
    // If there is an instance, and it's not me, delete myself.
    if (instance != null && instance != this) 
        Destroy(this); 
    else 
        instance = this; 
    }

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

    public void PlaceObject()
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
                buildObjDataList.Add(new BuildObjData(buildIndex, buildObj.transform.position, buildObj.transform.rotation));
                Debug.Log("buildObjDataList.Count "+buildObjDataList.Count);
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

    public void Save()
    {
        if (buildObjDataList != null)
        {
            Debug.Log("ตรงนี้1");
            BuildObjData.Save(buildObjDataList);
        }
        else
        {
            Debug.LogWarning("buildObjDataList is null, cannot save data.");
        }
    }



    public void Load()
    {
        List<BuildObjData> dataToLoad = BuildObjData.Load();
        if (dataToLoad.Count > 0)
        {
            Debug.Log("ตรงนี้5");
            buildObjDataList = dataToLoad;
            foreach (BuildObjData data in dataToLoad)
            {
                GameObject buildObj = (GameObject)Instantiate(buildPreset[data.index].prefab, data.position, data.rotation);
                buildObj.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                buildObj.transform.DOScale(1, 0.5f).SetEase(customEase);
            }
        }
    }

}

[Serializable]
public class BuildObjData {

    public BuildObjData(int _index, Vector3 _position, Quaternion _rotation)
    {
        index = _index;
        position = _position;
        rotation = _rotation;
    }

    public static void Save(List<BuildObjData> _buildObjDataList)
    {
        Debug.Log("ตรงนี้2 " + _buildObjDataList.Count);
        FileHandler.SaveToJSON<List<BuildObjData>>(_buildObjDataList, "BuildObj.json");
    }

    public static List<BuildObjData> Load()
    {
        List<BuildObjData> dataToLoad = FileHandler.ReadFromJSON<List<BuildObjData>>("BuildObj.json");
        Debug.Log("dataToLoad "+ dataToLoad);
        if (dataToLoad != null)
        {
            Debug.Log("ตรงนี้3");
            return dataToLoad;
        }
        else
        {
            Debug.Log("ตรงนี้4");
            return new List<BuildObjData>();
        }
    }

    public int index;
    public Vector3 position;
    public Quaternion rotation;
}



