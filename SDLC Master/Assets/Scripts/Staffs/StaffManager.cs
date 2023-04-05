using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{   
    public static StaffManager instance;
    public GameObject staffPrefab;
    public Sprite[] positionLogos;
    public Color[] positionColors;



    // Start is called before the first frame update
    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getTotalStaff()
    {
        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        return staffs.Length;
    }

    public int getTotalStaffbyPosition(string position)
    {
        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        int sum = 0;
        for(int i=0; i<staffs.Length; i++)
        {
            if(staffs[i].GetComponent<StaffProperties>().position == position)
                sum++;
        }
        return sum;
    }


    public List<StaffProperties> getAllStaff()
    {
        List<StaffProperties> staffProperties = new List<StaffProperties>();
        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        for(int i=0; i<staffs.Length; i++)
        {
            staffProperties.Add(staffs[i].GetComponent<StaffProperties>());
        }
        return staffProperties;
    }

    public int getStaffStat(StaffProperties staff, string position)
    {
        switch(position)
        {
            case "Analyst":
                return staff.analysis;
            case "Designer":
                return staff.design;
            case "Programmer":
                return staff.coding;
            case "Tester":
                return staff.testing;
        }
        return 0;
    }

    public int getSumStaffStat(string position)
    {
        int sum = 0;
        foreach(var staff in getAllStaff())
        {
            if(staff.position == position)
            {
                switch(position)
                {
                    case "Analyst":
                        sum += staff.analysis;
                        break;
                    case "Designer":
                        sum += staff.design;
                        break;
                    case "Programmer":
                        sum += staff.coding;
                        break;
                    case "Tester":
                        sum += staff.testing;
                        break;
                }
            }
        }
        return sum;
    }

    public int getAllStaffStat()
    {
        int sum = 0;
        foreach(var staff in getAllStaff())
        {
            sum += staff.analysis;
        }
        return sum;
    }

    public StaffProperties GetStaffbyID(string id)
    {
        GameObject[] staffs = GameObject.FindGameObjectsWithTag("Staff");
        for(int i=0; i<staffs.Length; i++)
        {
            if(staffs[i].GetComponent<StaffProperties>().id == id)
                return staffs[i].GetComponent<StaffProperties>();
        }
        return null;
    }

    public void TrainStaff(StaffProperties staff){
        Debug.Log(staff.fname);
        staff.analysis += 2;
        staff.design += 2;
        staff.coding += 2;
        staff.testing += 2;
    }

    public void KickoutStaff(StaffProperties staff){
        Destroy(staff.gameObject);
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