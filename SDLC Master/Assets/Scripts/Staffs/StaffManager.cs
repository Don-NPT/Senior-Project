using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{   
    public static StaffManager instance;
    List<StaffProperties> staffProperties;
    public GameObject staffPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;
        
        staffProperties = new List<StaffProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<StaffProperties> getAllStaff()
    {
        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        for(int i=0; i<staffs.Length; i++)
        {
            staffProperties.Add(staffs[i].GetComponent<StaffProperties>());
        }
        return staffProperties;
    }

    public List<Staff> getAllSerializableStaff()
    {
        List<Staff> serializableStaff = new List<Staff>();
        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        for(int i=0; i<staffs.Length; i++)
        {
            serializableStaff.Add(staffs[i].GetComponent<StaffProperties>().getSerializable());
        }
        return serializableStaff;
    }

    public void Save()
    {
        StaffAdapter staffAdapter = new StaffAdapter();
        staffAdapter.Save(getAllSerializableStaff());
    }

    public void Load()
    {
        StaffAdapter gameAdapter = new StaffAdapter();
        StaffAdapter saveData = gameAdapter.Load();

        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        foreach(var staff in staffs)
        {
            Destroy(staff.gameObject);
        }

        foreach(var saveStaff in saveData.staff)
        {
            Vector3 position = new Vector3(saveStaff.location[0],saveStaff.location[1],saveStaff.location[2]);
            Quaternion rotation = new Quaternion(saveStaff.rotation[0],saveStaff.rotation[1],saveStaff.rotation[2],saveStaff.rotation[3]);
            GameObject staff = (GameObject)Instantiate(staffPrefab, position, rotation);

            staff.GetComponent<StaffProperties>().setData(saveStaff);
        }
        // money = saveData.money;
    }

}

[System.Serializable]
public class StaffAdapter
{
    public List<Staff> staff;

    public void Save(List<Staff> _staff)
    {
        staff = _staff;
        FileHandler.SaveToJSON<StaffAdapter> (this, "staff.json");   
    }

    public StaffAdapter Load()
    {
        StaffAdapter dataToLoad = FileHandler.ReadFromJSON<StaffAdapter> ("staff.json");
        return dataToLoad;
    }
}