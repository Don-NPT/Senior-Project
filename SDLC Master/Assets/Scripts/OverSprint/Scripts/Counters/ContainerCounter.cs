
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnplayerGrabbedObject;

    [SerializeField] public KitchenObjectSO kitchenObjectSO;

    private Project project;
    public int numTasks;

    private void Start()
    {
        project = ProjectManager.instance.currentProject;

        switch(kitchenObjectSO.objectName){
            case "งานขนาดเล็ก":
                numTasks = project.smallTasks;
                GetComponentInChildren<TextMeshProUGUI>().text = project.smallTasks.ToString();
                break;
            case "งานขนาดกลาง":
                numTasks = project.mediumTasks;
                GetComponentInChildren<TextMeshProUGUI>().text = project.mediumTasks.ToString();
                break;
            case "งานขนาดใหญ่":
                numTasks = project.largeTasks;
                GetComponentInChildren<TextMeshProUGUI>().text = project.largeTasks.ToString();
                break;
            case "งานแบบเบิ้มๆ":
                numTasks = project.giantTasks;
                GetComponentInChildren<TextMeshProUGUI>().text = project.giantTasks.ToString();
                break;
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject() && numTasks > 0){
            numTasks--;
            GetComponentInChildren<TextMeshProUGUI>().text = numTasks.ToString();
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefeb);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnplayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            Debug.Log("numoftasksmall "+ numTasks);
    }

}
}