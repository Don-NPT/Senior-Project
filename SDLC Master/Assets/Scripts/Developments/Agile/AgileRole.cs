using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AgileRole : MonoBehaviour
{
    public GameObject PO_block;
    public GameObject scrumMaster_block;
    public Transform devTeam_slot;
    public GameObject devTeam_prefab;
    private GameObject[] devTeam_blocks;
    public Button nextBTN;
    private string PO_id;
    
    void OnEnable()
    {
        Debug.Log("TEst");
        // Setup Scrum Master
        scrumMaster_block.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Player";
        scrumMaster_block.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Lv.1";

        // Clear PO slot
        PO_block.SetActive(false);
        PO_block.GetComponent<Button>().onClick.AddListener(delegate {UnselectPO();});

        // Clear devTeam slot
        foreach(Transform child in devTeam_slot){
            Destroy(child.gameObject);
        }

        // Add devTeam blocks into the slot
        devTeam_blocks = new GameObject[StaffManager.instance.getTotalStaff()];
        List<StaffProperties> staffs = StaffManager.instance.getAllStaff();
        for(int i=0; i<StaffManager.instance.getTotalStaff(); i++){
            devTeam_blocks[i] = (GameObject)Instantiate(devTeam_prefab);
            devTeam_blocks[i].transform.SetParent(devTeam_slot);
            devTeam_blocks[i].transform.localScale = Vector3.one;

            devTeam_blocks[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = staffs[i].fname;
            devTeam_blocks[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffs[i].GetStaffStat().ToString();
            devTeam_blocks[i].GetComponentsInChildren<Image>()[1].sprite = staffs[i].GetStaffLogo();
            devTeam_blocks[i].GetComponentsInChildren<Image>()[1].color = staffs[i].GetStaffColor();
            int index = i;
            devTeam_blocks[i].GetComponent<Button>().onClick.AddListener(delegate {SelectPO(staffs[index]);});
        }

        nextBTN.onClick.AddListener(delegate {Next();});
    }

    void SelectPO(StaffProperties staff){
        // Set PO block
        PO_id = staff.id;
        PO_block.SetActive(true);
        PO_block.GetComponentsInChildren<TextMeshProUGUI>()[0].text = staff.fname;
        PO_block.GetComponentsInChildren<TextMeshProUGUI>()[1].text = staff.analysis.ToString();
        PO_block.GetComponentsInChildren<Image>()[1].sprite = StaffManager.instance.positionLogos[0];
        PO_block.GetComponentsInChildren<Image>()[1].color = StaffManager.instance.positionColors[0];

        ClearDevTeam();

        //Reset devTeam
        devTeam_blocks = new GameObject[StaffManager.instance.getTotalStaff()];
        List<StaffProperties> staffs = StaffManager.instance.getAllStaff();
        for(int i=0; i<StaffManager.instance.getTotalStaff(); i++){
            if(staffs[i].id != staff.id){
                devTeam_blocks[i] = (GameObject)Instantiate(devTeam_prefab);
                devTeam_blocks[i].transform.SetParent(devTeam_slot);
                devTeam_blocks[i].transform.localScale = Vector3.one;

                devTeam_blocks[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = staffs[i].fname;
                devTeam_blocks[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffs[i].GetStaffStat().ToString();
                devTeam_blocks[i].GetComponentsInChildren<Image>()[1].sprite = staffs[i].GetStaffLogo();
                devTeam_blocks[i].GetComponentsInChildren<Image>()[1].color = staffs[i].GetStaffColor();
                int index = i;
                devTeam_blocks[i].GetComponent<Button>().onClick.AddListener(delegate {
                    SelectPO(staffs[index]);
                });
            }
        }
    }

    void UnselectPO(){
        PO_id = "";
        PO_block.SetActive(false);
        
        ClearDevTeam();

        // Reset devTeam blocks into the slot
        devTeam_blocks = new GameObject[StaffManager.instance.getTotalStaff()];
        List<StaffProperties> staffs = StaffManager.instance.getAllStaff();
        for(int i=0; i<StaffManager.instance.getTotalStaff(); i++){
            devTeam_blocks[i] = (GameObject)Instantiate(devTeam_prefab);
            devTeam_blocks[i].transform.SetParent(devTeam_slot);
            devTeam_blocks[i].transform.localScale = Vector3.one;

            devTeam_blocks[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = staffs[i].fname;
            devTeam_blocks[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = staffs[i].GetStaffStat().ToString();
            devTeam_blocks[i].GetComponentsInChildren<Image>()[1].sprite = staffs[i].GetStaffLogo();
            devTeam_blocks[i].GetComponentsInChildren<Image>()[1].color = staffs[i].GetStaffColor();
            int index = i;
            devTeam_blocks[i].GetComponent<Button>().onClick.AddListener(delegate {
                SelectPO(staffs[index]);
            });
        }
    }

    void Next(){
        ProjectManager.instance.currentProject.PO_id = PO_id;
    }

    void ClearDevTeam(){
        foreach(GameObject child in devTeam_blocks){
            Destroy(child);
        }
    }

}
