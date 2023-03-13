using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskInfo : MonoBehaviour
{

    // Start is called before the first frame update
    void OnEnable() {
        GetComponentsInChildren<TextMeshProUGUI>()[2].text = AgileManager.instance.project.smallTasks.ToString();
        GetComponentsInChildren<TextMeshProUGUI>()[4].text = AgileManager.instance.project.mediumTasks.ToString();
        GetComponentsInChildren<TextMeshProUGUI>()[6].text = AgileManager.instance.project.largeTasks.ToString();
        GetComponentsInChildren<TextMeshProUGUI>()[8].text = AgileManager.instance.project.giantTasks.ToString();
    }

}
