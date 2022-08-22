using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public enum StaffState {IDLE, WORKING, COMPLETE}

public class StaffBehavior : MonoBehaviour
{
    public GameObject uiPrefab;
    public GameObject ui;
    public Vector3 offsetY = new Vector3(0, 2.1f, 0);
    // public Ease customEase = Ease.OutBack;
    private bool showUI = false;
    public GameObject progressBarPrefab;
    private GameObject progressBar;
    public StaffState state;

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
            progressBar.GetComponent<ProgressBar>().SetupBar(100, 5f);
        }
        if(progressBar.GetComponent<ProgressBar>().IsCompleted())
        {
            state = StaffState.COMPLETE;
        }

        progressBar.GetComponent<ProgressBar>().UpdateBar();
        // progressBar.GetComponent<ProgressBar>().UpdateBar();
        
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
                    // Debug.Log("Click!!!");
                    // if(ui == null)
                    // {
                    //     ui = (GameObject)Instantiate(uiPrefab, FindObjectOfType<Canvas>().transform);
                    //     foreach(CommandBar child in ui.GetComponentsInChildren<CommandBar>()){
                    //         child.Setup(gameObject);
                    //     }
                    //     // ui.gameObject.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                    //     // ui.gameObject.transform.DOScale(1, 0.5f).SetEase(customEase);
                    //     RectTransform[] childrenTransform = ui.GetComponentsInChildren<RectTransform>();
                    //     StartCoroutine(ChildPopup(childrenTransform));
                        
                    //     showUI = true;
                    // }
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
                    //  Debug.Log("Click outside");
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

    public void AssignWork(){
        state = StaffState.WORKING;
    }

    public void DestroyUI()
    {
        if(ui != null)
        {
            showUI = false;
            ui.gameObject.transform.DOScale(0, 0.05f).SetEase(Ease.InBounce);
            // ui.gameObject.transform.DOKill(false);
            Destroy(ui.gameObject, 0.7f);
        }
    }

}
