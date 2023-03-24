
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnplayerGrabbedObject;

    [SerializeField] public KitchenObjectSO kitchenObjectSO;

    private Project project;
    public int numoftasksmall;
    public int numoftaskmedium;
    public int numoftasklarge;
    public int numoftasksgiant;

    private void Start()
    {
        project = ProjectManager.instance.currentProject;
        numoftasksmall = project.smallTasks;
        numoftaskmedium = project.mediumTasks;
        numoftasklarge = project.largeTasks;
        numoftasksgiant = project.giantTasks;
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject()){
            //ไม่ได้ถืออะไรอยู่
            if(kitchenObjectSO.objectName == "งานขนาดเล็ก" && numoftasksmall > 0){
                numoftasksmall--;
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefeb);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                OnplayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                Debug.Log("numoftasksmall "+ numoftasksmall);
            }
            if(kitchenObjectSO.objectName == "งานขนาดกลาง" && numoftaskmedium > 0){
                numoftaskmedium--;
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefeb);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                OnplayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                Debug.Log("numoftaskmedium "+ numoftaskmedium);
            }
            if(kitchenObjectSO.objectName == "งานขนาดใหญ่" && numoftasklarge > 0){
                numoftasklarge--;
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefeb);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                OnplayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                Debug.Log("numoftasklarge "+ numoftasklarge);
            }
            if(kitchenObjectSO.objectName == "งานแบบเบิ้มๆ" && numoftasksgiant > 0){
                numoftasksgiant--;
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefeb);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                OnplayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                Debug.Log("numoftasksgiant "+ numoftasksgiant);
            }
    }

}
}