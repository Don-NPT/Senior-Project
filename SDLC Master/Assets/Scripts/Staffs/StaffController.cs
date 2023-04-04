using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public enum StaffState {IDLE, WAITING, WORKING, COMPLETE}

public class StaffController : MonoBehaviour
{
    public GameObject uiPrefab;
    private GameObject ui;
    public Vector3 offsetY = new Vector3(0, 2.1f, 0);
    // public Ease customEase = Ease.OutBack;
    private bool showUI = false;
    public GameObject progressBarPrefab;
    private GameObject progressBar;
    public StaffState state;

    // Update is called once per frame
    void Update()
    {
        CheckClick();
        if(ui != null)
        {
            ui.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsetY);
        }
        if(showUI == false){
            DestroyUI();
        }
        if(state == StaffState.WORKING){
            ManageProgressBar();
        }
    }

    void ManageProgressBar()
    {
        if(progressBar == null)
        {
            progressBar = (GameObject)Instantiate(progressBarPrefab, FindObjectOfType<Canvas>().transform);
            progressBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offsetY);
            progressBar.GetComponent<ProgressBar>().SetupBarWithColor(DevelopmentManager.instance.DayEachPhase, 0.3f, DevelopmentManager.instance.currentPhase);
        }
        if(progressBar.GetComponent<ProgressBar>().IsCompleted())
        {
            state = StaffState.COMPLETE;
        }
        // progressBar.GetComponent<ProgressBar>().UpdateBar(); 
        progressBar.GetComponentInChildren<Slider>().DOValue(DevelopmentManager.instance.currentDayInPhase, 0.3f).Play();       
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
                        // foreach(CommandBar child in ui.GetComponentsInChildren<CommandBar>()){
                        //     child.Setup(gameObject);
                        // }
                        ui.GetComponent<StaffBar>().SetupWithUI(gameObject, ui);
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

    public void AssignWork(){
        state = StaffState.WAITING;
    }

    public void DestroyUI()
    {
        if(ui != null)
        {
            showUI = false;
            // ui.gameObject.transform.DOScale(0, 0.05f).SetEase(Ease.InBounce);
            // ui.gameObject.transform.DOKill(false);
            Destroy(ui.gameObject, 0.2f);
        }
    }

}
