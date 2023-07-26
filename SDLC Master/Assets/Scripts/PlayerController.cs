using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject ui;
    public Vector3 offsetY = new Vector3(0, 2.1f, 0);
    private bool showUI = false;

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
        if(showUI == false){
            ui.SetActive(false);
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
                    if(!ui.activeSelf)
                    {
                        Debug.Log("Click!!!");
                        ui.SetActive(true);
                        RectTransform[] childrenTransform = ui.GetComponentsInChildren<RectTransform>();
                        StartCoroutine(ChildPopup(childrenTransform));
                    }
                    FindObjectOfType<AudioManager>().Play("Click");
                    showUI = true;
                 }
                 else
                 {
                    ui.SetActive(false);
                 }
             }
             else
             {
                ui.SetActive(false);
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
            ui.gameObject.transform.DOScale(0, 0.05f).SetEase(Ease.InBounce);
            Destroy(ui.gameObject, 0.7f);
        }
    }

}
