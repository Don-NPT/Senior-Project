using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class Prop : MonoBehaviour
{
    public GameObject uiPrefab;
    private GameObject ui;
    private bool showUI = false;
    public Vector3 offsetY = new Vector3(0, 2.1f, 0);
    public int price;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckClick();
        if(ui != null)
        {
            ui.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsetY);
        }

    }
    
    private void CheckClick(){
        if (Input.GetButtonDown("Fire1")  && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit = new RaycastHit();      
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
 
             if (Physics.Raycast(ray, out hit))
             {
                 if (hit.transform.gameObject == this.gameObject)
                 {
                    
                    if(ui == null)
                    {
                        ui = (GameObject)Instantiate(uiPrefab, FindObjectOfType<Canvas>().transform);
                        foreach(CommandBar child in ui.GetComponentsInChildren<CommandBar>()){
                            child.SetupWithUI(gameObject, ui);
                        }
                        RectTransform[] childrenTransform = ui.GetComponentsInChildren<RectTransform>();
                        StartCoroutine(ChildPopup(childrenTransform));
                        
                        showUI = true;
                    }
                    FindObjectOfType<AudioManager>().Play("Click");
                    showUI = true;
                 }
                 else
                 {
                    DestroyUI();
                 }
             }
             else
             {
                DestroyUI();
                Debug.Log("Click outside of any object");
             }
        }else if(Input.GetButtonDown("Fire1")){
            showUI = false;
        }
    }

    IEnumerator ChildPopup(RectTransform[] childrenTransform)
    {
        foreach(var child in childrenTransform)
        {
            child.localScale = Vector3.zero;
        }
        foreach(var child in childrenTransform)
        {
            child.DOScale(1, 0.2f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void DestroyUI()
    {
        if(ui != null)
        {
            showUI = false;
            Destroy(ui.gameObject);
        }
    }
}



