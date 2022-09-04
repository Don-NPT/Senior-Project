using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectManager : MonoBehaviour
{
    //[SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private LayerMask clickableLayer;
    //[SerializeField] private Material highlightMaterial;
    //[SerializeField] private Material defaultMaterial;

    //private Transform _selection;
   // public ProjectPreset[] projectpreset;//store project selected
    //private bool isSelected;
    //private Camera cam;
    //public GameObject itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
       // cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, clickableLayer))
            {
                rayHit.collider.GetComponent<ClickOn>().ClickMe();
            }
        }
            
        
    }

}
