using UnityEngine;

public class StaffProperties : MonoBehaviour
{
    public string id;
    public string fname;
    public int coding;
    public int design;
    public int testing;
    public int analysis;
    public float stamina;
    public int wage;
    public string position;
    public bool isAssign;
    public bool inTeam;
    public int teamIndex;
    public Vector3 location;
    public Quaternion rotation;

    public int GetStaffStat(){
        switch(position)
            {
                case "Analyst":
                    return analysis;
                case "Designer":
                    return design;
                case "Programmer":
                    return coding;
                case "Tester":
                    return testing;
            }
        return 0;
    }

    public Sprite GetStaffLogo(){
        switch(position)
            {
                case "Analyst":
                    return StaffManager.instance.positionLogos[0];
                case "Designer":
                    return StaffManager.instance.positionLogos[1];
                case "Programmer":
                    return StaffManager.instance.positionLogos[2];
                case "Tester":
                    return StaffManager.instance.positionLogos[3];
            }
        return null;
    }

    public Color GetStaffColor(){
        switch(position)
            {
                case "Analyst":
                    return StaffManager.instance.positionColors[0];
                case "Designer":
                    return StaffManager.instance.positionColors[1];
                case "Programmer":
                    return StaffManager.instance.positionColors[2];
                case "Tester":
                    return StaffManager.instance.positionColors[3];
            }
        return Color.white;
    }

    public Staff getSerializable()
    {
        location = transform.position;
        rotation = transform.rotation;

        Staff staff = new Staff();
        staff.setData(this);
        return staff;
    }

    public void setData(Staff staff)
    {
        id = staff.id;
        fname = staff.fname;
        coding = staff.coding;
        design = staff.design;
        testing = staff.testing;
        analysis = staff.analysis;
        wage = staff.wage;
        stamina = staff.stamina;
        position = staff.position;
        isAssign = staff.isAssign;
        inTeam = staff.inTeam;

        location = new Vector3(staff.location[0], staff.location[1], staff.location[2]);
        rotation = new Quaternion(staff.rotation[0], staff.rotation[1], staff.rotation[2], staff.rotation[3]);
    }

}

[System.Serializable]
public class Staff
{
    public string id;
    public string fname;
    public int analysis;
    public int design;
    public int coding;
    public int testing;
    public int wage;
    public float stamina;
    public string position;
    public bool isAssign;
    public bool inTeam;
    public float[] location;
    public float[] rotation;

    public void setData(StaffProperties staffProperties)
    {
        id = staffProperties.id;
        fname = staffProperties.fname;
        coding = staffProperties.coding;
        design = staffProperties.design;
        testing = staffProperties.testing;
        analysis = staffProperties.analysis;
        wage = staffProperties.wage;
        stamina = staffProperties.stamina;
        position = staffProperties.position;
        isAssign = staffProperties.isAssign;
        inTeam = staffProperties.inTeam;

        location = new float[3];
        location[0] = staffProperties.location.x;
        location[1] = staffProperties.location.y;
        location[2] = staffProperties.location.z;
        
        rotation = new float[4];
        rotation[0] = staffProperties.rotation.x;
        rotation[1] = staffProperties.rotation.y;
        rotation[2] = staffProperties.rotation.z;
        rotation[3] = staffProperties.rotation.w;
    }

}
