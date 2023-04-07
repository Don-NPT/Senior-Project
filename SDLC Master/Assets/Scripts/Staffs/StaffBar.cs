using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StaffBar : MonoBehaviour
{
    private GameObject staffGO;
    private GameObject staffUIGO;
    public GameObject floatingText;
    public GameObject floatingText2;
    public GameObject trainFX;
    public GameObject trainFX2;
    public Vector3 trainFXOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetupWithUI(GameObject GO, GameObject ui)
    {
        staffGO = GO;
        staffUIGO = ui;
        // GetComponentInParent<Transform>().position = Camera.main.WorldToScreenPoint(GO.transform.position + offsetY);
        // Debug.Log(GetComponentInParent<Transform>().position);
        Button[] buttons = GetComponentsInChildren<Button>();
        
        buttons[0].onClick.AddListener(delegate { 
            Debug.Log("Show staff detail");
            StaffManager.instance.ShowStaffDetail(staffGO.GetComponent<StaffProperties>());
        });
        buttons[1].onClick.AddListener(delegate { 
            Debug.Log("Train staff");
            if(GameManager.instance.getMoney() > 10000){
                GameManager.instance.PayTrainStaff();
                StaffManager.instance.TrainStaff(staffGO.GetComponent<StaffProperties>());
                CreateText(1);
                CreateFX(1);
            }
            else{
                Debug.Log("Money not enough to Train");
                CreateText(2);
                CreateFX(2);
            }
        });
        buttons[2].onClick.AddListener(delegate { 
            Debug.Log("Kick out staff");
            StaffManager.instance.KickoutStaff(staffGO.GetComponent<StaffProperties>());
        });
    }

    public void CreateText(int a){
        if(a == 1){
        // GameObject text = floatingText;
        GameObject textGO = (GameObject)Instantiate(floatingText, FindObjectOfType<Canvas>().transform);
        textGO.transform.position = Camera.main.WorldToScreenPoint(staffGO.transform.position + new Vector3(0, 2.1f, 0));
        textGO.transform.DOPunchScale(new Vector3 (0.2f, 0.2f, 0.2f), .25f);
        Destroy(textGO, 2f);
        }else{
        GameObject textGO = (GameObject)Instantiate(floatingText2, FindObjectOfType<Canvas>().transform);
        textGO.transform.position = Camera.main.WorldToScreenPoint(staffGO.transform.position + new Vector3(0, 2.1f, 0));
        textGO.transform.DOPunchScale(new Vector3 (0.2f, 0.2f, 0.2f), .25f);
        Destroy(textGO, 2f);
        }
    }

    void CreateFX(int a){
        if(a == 1){
            GameObject fx = (GameObject)Instantiate(trainFX);
            fx.transform.position = staffGO.transform.position;
            Destroy(fx, 2f);  
        }else{
            GameObject fx = (GameObject)Instantiate(trainFX2);
            fx.transform.position = staffGO.transform.position;
            Destroy(fx, 2f);  
        }
    }

}
